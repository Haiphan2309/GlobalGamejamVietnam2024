using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Shun_Draggable_System
{
    [RequireComponent(typeof(Collider2D))]
    public class BaseDraggableRegion : MonoBehaviour, IHoverable
    {
        public enum MiddleInsertionStyle
        {
            AlwaysBack,
            RoundUpSlot,
            Cannot,
            Swap,
            NearestRoundSlot,
            RoundDownSlot
        }

        public enum RegionDirection
        {
            LeftToRight,
            RightToLeft,
        }
        
        
        [SerializeField]
        private bool _interactable = true;
        [SerializeField] protected BaseDraggableHolder DraggableHolderPrefab;
        [SerializeField] protected Transform SpawnPlace;
        [SerializeField] protected Vector3 DraggableOffset = new Vector3(5f, 0 ,0);

        
        [SerializeField] protected List<BaseDraggableHolder> DraggablePlaceHolders = new();
        [SerializeField] protected int MaxDraggableHold;
        
        public MiddleInsertionStyle DraggableMiddleInsertionStyle = MiddleInsertionStyle.NearestRoundSlot;
        
        
        
        public int DraggableHoldingCount { get; private set; } 
        
        public bool IsHoverable { get => _interactable; protected set => _interactable = value;}
        public bool IsHovering { get; protected set; }

        
        protected BaseDraggableHolder TemporaryBaseDraggableHolder;

        
        #region INITIALIZE

        protected virtual void Awake()
        {
            InitializeDraggablePlaceHolder();
        }

        protected virtual void InitializeDraggablePlaceHolder()
        {
            if (DraggablePlaceHolders.Count != 0)
            {
                MaxDraggableHold = DraggablePlaceHolders.Count;
                for (int i = 0; i < MaxDraggableHold; i++)
                {
                    DraggablePlaceHolders[i].InitializeRegion(this, i);
                }
            }
            else
            {
                for (int i = 0; i < MaxDraggableHold; i++)
                {
                    var cardPlaceHolder = Instantiate(DraggableHolderPrefab, SpawnPlace.position + ((float)i -  MaxDraggableHold/2f) * DraggableOffset,
                        Quaternion.identity, SpawnPlace);
                    DraggablePlaceHolders.Add(cardPlaceHolder);
                    cardPlaceHolder.InitializeRegion(this, i);
                }    
            }
            
        }
        
        #endregion

        #region OPERATION

        
        public List<BaseDraggable> GetAllDraggableGameObjects(bool getNull = false)
        {
            List<BaseDraggable> result = new();
            for (int i = 0; i < DraggableHoldingCount; i++)
            {
                if ((!getNull && DraggablePlaceHolders[i].Draggable != null) || getNull) result.Add(DraggablePlaceHolders[i].Draggable);
            }

            return result;
        }

        public void DestroyAllDraggableGameObject()
        {
            foreach (var cardHolder in DraggablePlaceHolders)
            {
                if (cardHolder.Draggable == null) continue;
                Destroy(cardHolder.Draggable.gameObject);
                cardHolder.Draggable = null;
            }
            
            DraggableHoldingCount = 0;
        }

        protected BaseDraggableHolder FindEmptyDraggablePlaceHolder()
        {
            if (DraggableHoldingCount >= MaxDraggableHold) return null;
            return DraggablePlaceHolders[DraggableHoldingCount];
        }
        
        public BaseDraggableHolder FindDraggablePlaceHolder(BaseDraggable baseDraggable)
        {
            foreach (var cardPlaceHolder in DraggablePlaceHolders)
            {
                if (cardPlaceHolder.Draggable == baseDraggable) return cardPlaceHolder;
            }

            return null;
        }

        public bool AddDraggable(BaseDraggable draggable, BaseDraggableHolder draggableHolder = null, bool isReAdd = false)
        {
            if ( draggableHolder == null || draggableHolder.IndexInRegion >= DraggableHoldingCount)
            {
                return AddDraggableAtBack(draggable, isReAdd);
            }

            return DraggableMiddleInsertionStyle switch
            {
                MiddleInsertionStyle.AlwaysBack => AddDraggableAtBack(draggable, isReAdd),
                MiddleInsertionStyle.RoundUpSlot => AddDraggableAtMiddle(draggable, draggableHolder.IndexInRegion, isReAdd),
                MiddleInsertionStyle.NearestRoundSlot => AddDraggableAtMiddle(draggable,GetRoundIndex(draggable, draggableHolder) , isReAdd),
                MiddleInsertionStyle.RoundDownSlot => AddDraggableAtMiddle(draggable, draggableHolder.IndexInRegion-1, isReAdd),
                MiddleInsertionStyle.Cannot => false,
                _ => false
            };
        }

        private int GetRoundIndex(BaseDraggable draggable, BaseDraggableHolder draggableHolder)
        {
            var difference = draggable.transform.position - draggableHolder.transform.position;
            if (Vector3.Dot(difference, DraggableOffset) >= 0 )
            {
                return draggableHolder.IndexInRegion;
            }
            else
            {
                return draggableHolder.IndexInRegion - 1;
            }
            
        }

        private bool AddDraggableAtBack(BaseDraggable draggable, bool isReAdd = false)
        {
            if (DraggableHoldingCount >= MaxDraggableHold || !CheckCompatibleObject(draggable))
            {
                return false;
            }

            var index = DraggableHoldingCount;
            var cardPlaceHolder = DraggablePlaceHolders[index];
            cardPlaceHolder.AttachDraggableGameObject(draggable);
            
            DraggableHoldingCount ++;
            
            OnSuccessfullyAddDraggable(draggable, cardPlaceHolder, index, isReAdd);
                
            return true;
        }
        
        private  bool AddDraggableAtMiddle(BaseDraggable draggable, int index, bool isReAdd = false)
        {
            if (DraggableHoldingCount >= MaxDraggableHold || !CheckCompatibleObject(draggable))
            {
                return false;
            }
            
            ShiftRight(index);

            var cardPlaceHolder = DraggablePlaceHolders[index];
            cardPlaceHolder.AttachDraggableGameObject(draggable);
            
            DraggableHoldingCount++;
            
            OnSuccessfullyAddDraggable(draggable, cardPlaceHolder, index, isReAdd);
            
            return true;
        }
        
        
        protected virtual void ShiftRight(int startIndex)
        {
            startIndex = startIndex >= 0 ? startIndex : 0;
            for (int i = DraggablePlaceHolders.Count - 1; i > startIndex; i--)
            {
                var card = DraggablePlaceHolders[i - 1].DetachDraggableGameObject();
                
                if (card == null) continue;
                DraggablePlaceHolders[i].AttachDraggableGameObject(card);

            }
        }
        
        
        protected virtual void ShiftLeft(int startIndex)
        {
            startIndex = startIndex >= 0 ? startIndex : 0;
            for (int i = startIndex; i < DraggablePlaceHolders.Count - 1; i++)
            {
                var card = DraggablePlaceHolders[i + 1].DetachDraggableGameObject();
                
                if (card == null) continue;
                
                DraggablePlaceHolders[i].AttachDraggableGameObject(card);
                
                
                //SmoothMove(card.transform, _cardPlaceHolders[i].transform.position);

            }
        }
        
        public virtual bool RemoveDraggable(BaseDraggable draggable)
        {

            for (int i = 0; i < DraggablePlaceHolders.Count; i++)
            {
                if (DraggablePlaceHolders[i].Draggable != draggable) continue;
                DraggablePlaceHolders[i].DetachDraggableGameObject();
                
                ShiftLeft(i);
                DraggableHoldingCount--;
                
                OnSuccessfullyRemoveDraggable(draggable, DraggablePlaceHolders[i], i);
                return true;
            }
            return false;
        }
        
        public virtual bool RemoveDraggable(BaseDraggable draggable,BaseDraggableHolder draggableHolder, bool isTakeOutTemporary = false)
        {
            if (draggableHolder == null || draggableHolder.Draggable != draggable) return false;

            draggableHolder.DetachDraggableGameObject();

            var index = DraggablePlaceHolders.IndexOf(draggableHolder);
            ShiftLeft(index);
            DraggableHoldingCount--;

            OnSuccessfullyRemoveDraggable(draggable, draggableHolder, index, isTakeOutTemporary);
            return true;
        }
        
        
        #endregion

        #region MOUSE_INPUT
        
        public virtual bool TryAddDraggable(BaseDraggable draggable, BaseDraggableHolder draggableHolder = null)
        {
            if (!_interactable) return false;
            return AddDraggable(draggable, draggableHolder);
        }
        
        public virtual bool TakeOutTemporary(BaseDraggable draggable,BaseDraggableHolder draggableHolder)
        {
            if (!_interactable) return false;
            
            if (!RemoveDraggable(draggable, draggableHolder, true)) return false;
            
            TemporaryBaseDraggableHolder = draggableHolder;
            return true;
        }
        
        public virtual void ReAddTemporary(BaseDraggable baseDraggable)
        {
            if (baseDraggable == null) return;
            AddDraggable(baseDraggable, TemporaryBaseDraggableHolder, true);
            
            TemporaryBaseDraggableHolder = null;
        }

        public virtual void RemoveTemporary(BaseDraggable baseDraggable)
        {
            if (baseDraggable == null) return;
            
            TemporaryBaseDraggableHolder = null;
        }
        
        
        #endregion

        protected virtual void SmoothMove(Transform movingObject, Vector3 toPosition)
        {
            movingObject.position = toPosition;
        }

        public virtual bool CheckCompatibleObject(BaseDraggable baseDraggable)
        {
            return true;
        }
        
        private void RemoveDestroyedDraggable(BaseDraggable card)
        {
            RemoveDraggable(card);
        }
        
        protected virtual void OnSuccessfullyAddDraggable(BaseDraggable baseDraggable, BaseDraggableHolder baseDraggableHolder, int index, bool isReAdd = false)
        {
            if(!isReAdd) baseDraggable.OnDestroy += RemoveDestroyedDraggable;
        }

        protected virtual void OnSuccessfullyRemoveDraggable(BaseDraggable baseDraggable, BaseDraggableHolder baseDraggableHolder, int index, bool isTakeOutTemporary = false)
        {
            if(!isTakeOutTemporary) baseDraggable.OnDestroy -= RemoveDestroyedDraggable;
        }

        public void StartHover()
        {
            IsHovering = true;
            
        }

        public void EndHover()
        {
            IsHovering = false;
            
        }

        public virtual void DisableInteractable()
        {
            
            if (!_interactable) return;
            _interactable = false;
            if(IsHovering) EndHover();
        }
        
        public virtual void EnableInteractable()
        {
            if (_interactable) return;
            _interactable = true;
        }
    }
}