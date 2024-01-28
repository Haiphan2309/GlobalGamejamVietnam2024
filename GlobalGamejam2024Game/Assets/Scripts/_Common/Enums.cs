
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
        MAIN_MENU = 0,
        LEVEL_MENU,
        LEVEL_1,
        LEVEL_2,
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
        RICE,
        WOOL,
        SCARY_MASK,
        MILK,
        SOFA,

        MONEY = 100,
        CONFESSION,
        BUY,
        BENCH,
        APPLE,
        KNIFE,
        BALLOON,
        RAINY,
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
