namespace DG.Tweening.Core
{
    /// <summary>
    /// Added by Gavon 2018/1/30
    /// For NGUI
    /// </summary>
    public enum TargetTypeEx
    {
        Unset = 0,
        Camera = 1,
        CanvasGroup = 2,
        Image = 3,
        Light = 4,
        RectTransform = 5,
        Renderer = 6,
        SpriteRenderer = 7,
        Rigidbody = 8,
        Rigidbody2D = 9,
        Text = 10,
        Transform = 11,
        tk2dBaseSprite = 12,
        tk2dTextMesh = 13,
        TextMeshPro = 14,
        TextMeshProUGUI = 15,

        // added
        UISprite = 100,
        UITexture = 101,
        UIWidget = 102,

        Animation = 110,
        Animator = 111,
    }
}