using System.Collections;
using UnityEngine;

namespace MainGame.Card
{
    public interface ICardSkill
    {
        public IEnumerator UseCard();
        
    }
}