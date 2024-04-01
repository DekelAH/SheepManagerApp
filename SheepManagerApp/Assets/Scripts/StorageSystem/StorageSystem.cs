namespace Assets.Scripts.StorageSystem
{
    public abstract class StorageSystem
    {
        #region Methods

        public void SaveHerd()
        {
            SaveHerdInternal();
        }

        protected abstract void SaveHerdInternal();

        public void SaveUser()
        {
            SaveUserInternal();
        }

        protected abstract void SaveUserInternal();

        public void LoadHerd()
        {
            LoadHerdInternal();
        }

        protected abstract void LoadHerdInternal();

        public void LoadUser()
        {
            LoadUserInternal();
        }

        protected abstract void LoadUserInternal();

        #endregion
    }
}
