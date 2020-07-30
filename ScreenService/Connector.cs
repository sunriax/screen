using System;
using System.Device.Gpio;

namespace ScreenService
{
    public class Connector : IDisposable
    {
        private static GpioController _controller = new GpioController();

        private bool _disposed;

        public void Setup(int pin, PinMode mode)
        {
            if (!_controller.IsPinOpen(pin))
                _controller.OpenPin(pin, mode);
        }

        public void Status(int pin, PinValue value)
        {
            if (!_controller.IsPinOpen(pin))
                throw new Exception("Pin not opened");

            _controller.Write(pin, value);
        }

        public bool Status(int pin)
        {
            if (!_controller.IsPinOpen(pin))
                throw new Exception("Pin not opened");

            if (_controller.Read(pin) == PinValue.High)
                return true;
            return false;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _controller.Dispose();
            }
            _disposed = true;
        }
    }
}
