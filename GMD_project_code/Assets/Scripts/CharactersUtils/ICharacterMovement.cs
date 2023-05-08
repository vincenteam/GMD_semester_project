namespace GMDProject
{
    public interface ICharacterMovement
    {
        public void Jump();

        public void MoveRight();

        public void MoveLeft();

        public void MoveForward();

        public void MoveBackward();

        void RotateX(float amount);
        void RotateY(float amount);
        void RotateZ(float amount);
    }
}