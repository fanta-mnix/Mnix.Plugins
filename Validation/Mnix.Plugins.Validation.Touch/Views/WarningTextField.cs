using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using System;

namespace Mnix.Plugins.Validation.Touch.Views
{
	public class WarningTextField : UITextField
	{
		private UIFont mErrorFont = UIFont.SystemFontOfSize(12);
		public UIFont ErrorFont
		{
			get
			{
				return mErrorFont;
			}
			set
			{
				mErrorFont = value;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return ErrorContainer.Text;
			}
			set
			{
				ErrorContainer.Text = value;
				LayoutSubviews();

				if(IsFirstResponder)
				{
					SetErrorMessageVisible(HasError, false);
				}
			}
		}

		public bool HasError
		{
			get
			{
				return !(string.IsNullOrEmpty(ErrorMessage));
			}
		}

		private UIImageView mErrorIconView;
		private UIImageView ErrorIconView
		{
			get
			{
				if(mErrorIconView == null)
				{
					mErrorIconView = new UIImageView(ErrorIcon);
					mErrorIconView.SizeToFit();
					mErrorIconView.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
				}

				return mErrorIconView;
			}
		}

		private UIImage mErrorIcon;
		public UIImage ErrorIcon
		{
			get
			{
				if(mErrorIcon == null)
				{
					ErrorIcon = UIImage.FromBundle("Image/error.png");
				}

				return mErrorIcon;
			}
			set
			{
				mErrorIcon = value;
			}
		}

		private UITextView mErrorContainer;
		private UITextView ErrorContainer
		{
			get
			{
				if(mErrorContainer == null)
				{
					mErrorContainer = new UITextView();
					mErrorContainer.Font = ErrorFont;
					mErrorContainer.BackgroundColor = UIColor.Red;
					mErrorContainer.TextColor = UIColor.White;
					mErrorContainer.Hidden = true;
					mErrorContainer.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin;

					foreach(var touchWouch in mErrorContainer.GestureRecognizers)
					{
						mErrorContainer.RemoveGestureRecognizer(touchWouch);
					}

					UITapGestureRecognizer touchMe = new UITapGestureRecognizer((NSAction)
						delegate
						{
							SetErrorMessageVisible(false, true);
						});
					mErrorContainer.AddGestureRecognizer(touchMe);
				}

				return mErrorContainer;
			}
		}

		public WarningTextField(NSObjectFlag t) : base(t)
		{
		}

		public WarningTextField() : this(RectangleF.Empty)
		{
		}

		public WarningTextField(RectangleF frame) : base(frame)
		{
			ClipsToBounds = false;
			Add(ErrorContainer);

			EditingDidBegin += delegate(object sender, EventArgs e)
			{
				if(HasError)
				{
					SetErrorMessageVisible(true, true);
				}
			};

			EditingDidEnd += delegate(object sender, EventArgs e)
			{
				if(HasError)
				{
					SetErrorMessageVisible(false, true);
				}
			};
		}

		public WarningTextField(IntPtr handle) : base(handle)
		{
		}

		public WarningTextField(NSCoder coder) : base(coder)
		{
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			SetErrorIconVisible(HasError, true);
			ErrorContainer.Frame = CalculateErrorContainerFrame();
		}

		public override RectangleF TextRect(RectangleF forBounds)
		{
			if(HasError)
			{
				forBounds.Width = ErrorIconView.Frame.X - 5;
				return forBounds;
			}
			else
			{
				return base.TextRect(forBounds);
			}
		}

		private RectangleF CalculateErrorContainerFrame()
		{
			SizeF textSize = new SizeF(
                Frame.Width - ErrorContainer.ContentInset.Left - ErrorContainer.ContentInset.Right - 18,
				float.MaxValue
			);
			textSize = ((NSString)ErrorMessage).StringSize(
				ErrorContainer.Font,
				textSize,
				UILineBreakMode.WordWrap
			);
			textSize.Width = Frame.Width;
			textSize.Height += ErrorContainer.ContentInset.Top + ErrorContainer.ContentInset.Bottom + 18;

			return new RectangleF(
				new PointF(0, Frame.Height + 1),
				textSize
			);
		}

		public void SetErrorIconVisible(bool visible, bool animated)
		{
			if(visible)
			{
				ErrorIconView.Frame = new RectangleF(
					new PointF(
						Frame.Width - ErrorIconView.Frame.Width - 5,
						(Frame.Height - ErrorIconView.Frame.Height) / 2
					),
					ErrorIconView.Frame.Size
				);

				if(ErrorIconView.Superview == null)
				{
					ErrorIconView.Layer.Opacity = 0;
					Add(ErrorIconView);
					UIView.Animate(
						.3f,
						delegate {
							ErrorIconView.Layer.Opacity = 1;
						}
					);
				}
			}
			else
			{
				if(ErrorIconView.Superview != null)
				{
					UIView.Animate(
						.3f,
						delegate
						{
							ErrorIconView.Layer.Opacity = 0;
						},
						delegate
						{
							ErrorIconView.RemoveFromSuperview();
						});
				}
			}
		}

		public void SetErrorMessageVisible(bool visible, bool animated)
		{
			if(visible)
			{
				if(ErrorContainer.Hidden)
				{
					ErrorContainer.Layer.Opacity = 0;
					ErrorContainer.Hidden = false;

					int tolerance = 0;
					for (UIView superView = this; superView != null && tolerance++ < 5; superView = superView.Superview)
					{
						if (superView.Superview != null)
						{
							superView.Superview.BringSubviewToFront(superView);
						}
					}

					UIView.Animate(
						.3f,
						delegate
						{
							ErrorContainer.Layer.Opacity = 1;
						}
					);
				}
			}
			else
			{
				if(!ErrorContainer.Hidden)
				{
					UIView.Animate(
						.3f,
						delegate
						{
							ErrorContainer.Layer.Opacity = 0;
						},
						delegate
						{
							ErrorContainer.Hidden = true;
						});
				}
			}
		}
	}
}

