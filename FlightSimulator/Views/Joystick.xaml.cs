using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using FlightSimulator.Model.EventArgs;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        /// <summary>Current Aileron</summary>
        public static readonly DependencyProperty AileronProperty =
            DependencyProperty.Register("Aileron", typeof(double), typeof(Joystick), null);

        /// <summary>Current Elevator</summary>
        public static readonly DependencyProperty ElevatorProperty =
            DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);

        /// <summary>How often should be raised StickMove event in degrees</summary>
        public static readonly DependencyProperty AileronStepProperty =
            DependencyProperty.Register("AileronStep", typeof(double), typeof(Joystick), new PropertyMetadata(1.0));

        /// <summary>How often should be raised StickMove event in Elevator units</summary>
        public static readonly DependencyProperty ElevatorStepProperty =
            DependencyProperty.Register("ElevatorStep", typeof(double), typeof(Joystick), new PropertyMetadata(1.0))

        /// <summary>Current Aileron in degrees from 0 to 360</summary>
        public double Aileron
        {
            get { return Convert.ToDouble(GetValue(AileronProperty)); }
            set { SetValue(AileronProperty, value); }
        }

        /// <summary>current Elevator (or "power"), from 0 to 100</summary>
        public double Elevator
        {
            get { return Convert.ToDouble(GetValue(ElevatorProperty)); }
            set { SetValue(ElevatorProperty, value); }
        }

        /// <summary>How often should be raised StickMove event in degrees</summary>
        public double AileronStep
        {
            get { return Convert.ToDouble(GetValue(AileronStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 90) value = 90;
                SetValue(AileronStepProperty, Math.Round(value));
            }
        }

        /// <summary>How often should be raised StickMove event in Elevator units</summary>
        public double ElevatorStep
        {
            get { return Convert.ToDouble(GetValue(ElevatorStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 50) value = 50;
                SetValue(ElevatorStepProperty, value);
            }
        }

        /// <summary>
        /// OnScreenJoystickEventHandler(Joystick sender, VirtualJoystickEventArgs args).
        ///     Delegate holding data for joystick state change
        /// </summary>
        /// <param name="sender"> event's sender </param>
        /// <param name="args"> arguments </param>
        public delegate void OnScreenJoystickEventHandler(Joystick sender, VirtualJoystickEventArgs args);

        /// <summary>
        /// EmptyJoystickEventHandler(Joystick sender).
        ///     Delegate for joystick events that hold no data
        /// </summary>
        /// <param name="sender">The object that fired the event</param>
        public delegate void EmptyJoystickEventHandler(Joystick sender);

        /// <summary>update when hoystick moves</summary>
        public event OnScreenJoystickEventHandler Moved;

        /// <summary> update when joystick is released, and position is set </summary>
        public event EmptyJoystickEventHandler Released;

        /// <summary> update joystick pos captured </summary>
        public event EmptyJoystickEventHandler Captured;

        private Point m_startPos;
        private double m_prevAileron, m_prevElevator;
        private double m_canvasWidth, m_canvasHeight;
        private readonly Storyboard m_centerKnob;

        /// <summary>
        /// Joystick().
        /// </summary>
        public Joystick()
        {
            InitializeComponent();

            Knob.MouseLeftButtonDown += Knob_MouseLeftButtonDown;
            Knob.MouseLeftButtonUp += Knob_MouseLeftButtonUp;
            Knob.MouseMove += Knob_MouseMove;
            m_centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }

        /// <summary>
        /// Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_startPos = e.GetPosition(Base);
            m_prevAileron = m_prevElevator = 0;
            m_canvasWidth = Base.ActualWidth - KnobBase.ActualWidth;
            m_canvasHeight = Base.ActualHeight - KnobBase.ActualHeight;
            Captured?.Invoke(this);
            Knob.CaptureMouse();

            m_centerKnob.Stop();
        }

        /// <summary>
        /// Knob_MouseMove(object sender, MouseEventArgs e).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Knob.IsMouseCaptured)
                return;

            Point newPosition = e.GetPosition(Base);

            Point positionDelta = new Point(newPosition.X - m_startPos.X, newPosition.Y - m_startPos.Y);

            double distance = 
                Math.Round(
                    Math.Sqrt(
                        positionDelta.X * positionDelta.X + 
                        positionDelta.Y * positionDelta.Y));

            if (distance >= m_canvasWidth / 2 || distance >= m_canvasHeight / 2)
                return;

            Aileron = positionDelta.X / 124;
            Elevator = -positionDelta.Y / 124;

            knobPosition.X = positionDelta.X;
            knobPosition.Y = positionDelta.Y;

            if (Moved == null ||
                (!(Math.Abs(m_prevAileron - Aileron) > AileronStep) && !(Math.Abs(m_prevElevator - Elevator) > ElevatorStep)))
                return;

            Moved?.Invoke(this, new VirtualJoystickEventArgs { Aileron = Aileron, Elevator = Elevator });
            m_prevAileron = Aileron;
            m_prevElevator = Elevator;

        }

        /// <summary>
        /// Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Knob.ReleaseMouseCapture();
            m_centerKnob.Begin();
        }

        /// <summary>
        /// CenterKnob_Completed(object sender, EventArgs e).
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void CenterKnob_Completed(object sender, EventArgs e)
        {
            Aileron = 0;
            Elevator = 0;
            m_prevAileron = 0;
            m_prevElevator = 0;

            Released?.Invoke(this);
        }

    }
}