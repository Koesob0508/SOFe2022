namespace DNS
{
    public class TitleScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Title;
        }
        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}
