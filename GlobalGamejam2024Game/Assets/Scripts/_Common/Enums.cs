
namespace GDC.Enums
{
    public enum PointType
    {

    }
    public enum DialogueState
    {
        HEAD,
        BRANCH,
        TAIL,
    }
    public enum TransitionType
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        IN,
        FADE,
    }
    public enum SceneType
    {
        UNKNOWN = -999,
        MAIN = 0,
        HOME = 1,
        TUTORIAL = 2,
        INTRO = 3,
        __________REAL_WORLD_AREA__________ = 50,
        REAL_WORLD_AREA_01,
        __________BEGIN_AREA__________ = 100,
        BEGIN_AREA_1,
        BEGIN_AREA_2,
        BEGIN_AREA_3,
        BEGIN_AREA_4,
        BEGIN_AREA_5,
        BEGIN_AREA_6,
        BEGIN_AREA_7,
        BEGIN_AREA_8,
        BEGIN_AREA_HOUSE,
        __________AREA01__________ = 150,
        AREA1_1,
        AREA1_2,
        AREA1_3,
        AREA1_4,
        AREA1_5,
        AREA1_DUNGEON,
        AREA1_SUNFLOWER,
        AREA1_BONUS,
    }
    public enum CardType
    {
        CAT_PICTURE,
        DOG_PICTURE,
        MOUSE_PICTURE,
        PENGUIN_PICTURE,
        MOUSE,
        CAT_FOOD,
        BONE,
        BOWL_OF_RICE,
        WOOL,
        SCARE_MASK,
        MILK,
        SOFA,
    }
    public enum CharacterState
    {
        VERY_SAD,
        SAD,
        LITTLE_SAD,
        NORMAL,
        LITTLE_HAPPY,
        LAUGH,
    }
}
