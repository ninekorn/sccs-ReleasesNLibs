using SharpDX;
using SharpDX.Direct2D1;

namespace SC_skYaRk_VR_Edition_v005
{
    class SC_Console_SAMPLE2DRENDERER : SC_Console_DIRECT2DCOMPONENT
    {
        private Vector2 _position;
        private Vector2 _speed;
        private SolidColorBrush _circleColor;
        private SharpDX.Mathematics.Interop.RawRectangleF recter;
        private SolidColorBrush rectColor;

        public SC_Console_SAMPLE2DRENDERER()
        {
            _position = new Vector2(30, 30);
            _speed = new Vector2(0.5f, 0.2f);
            //recter.Bottom = 1;
            //recter.Right = 1;
        }

        protected override void InternalInitialize()
        {
            base.InternalInitialize();
            recter = new SharpDX.Mathematics.Interop.RawRectangleF(0, 0, 10, 10);
            //_circleColor = new SolidColorBrush(RenderTarget2D, new Color(1, 0.2f, 0.2f));
            rectColor = new SolidColorBrush(RenderTarget2D, new Color(0.5f, 1.0f, 0.75f));
        }

        protected override void InternalUninitialize()
        {
            Utilities.Dispose(ref _circleColor);

            base.InternalUninitialize();
        }

        protected override void Render()
        {
            UpdatePosition();
            RenderTarget2D.Clear(new Color(0.25f, 0.25f, 0.25f));
            RenderTarget2D.FillRectangle(recter, rectColor);

            //RenderTarget2D.Clear(new Color(1.0f, 0, 1.0f));
            //RenderTarget2D.FillEllipse(new Ellipse(_position, 20, 20), _circleColor);
        }

        private void UpdatePosition()
        {
            _position += _speed;

            if (_position.X > SurfaceWidth)
            {
                _position.X = SurfaceWidth;
                _speed.X = -_speed.X;
            }
            else if (_position.X < 0)
            {
                _position.X = 0;
                _speed.X = -_speed.X;
            }

            if (_position.Y > SurfaceHeight)
            {
                _position.Y = SurfaceHeight;
                _speed.Y = -_speed.Y;
            }
            else if (_position.Y < 0)
            {
                _position.Y = 0;
                _speed.Y = -_speed.Y;
            }
        }
    }
}
