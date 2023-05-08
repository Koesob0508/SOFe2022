namespace DNS
{
    public class EndScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.End;
        }
        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}
