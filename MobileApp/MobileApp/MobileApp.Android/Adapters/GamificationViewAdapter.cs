using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MobileApp.Droid.Views;
using Android.Graphics;
using Android.Content.Res;
using MobileApp.Constants;
using System.Collections;

namespace MobileApp.Droid.Adapters
{
    public class GamificationViewAdapter : BaseAdapter 
    {
        private NonAdminDashBoardView _context;
        private View _view;
		private GridLayout _puzzleLayout;
		private int _gameViewWidth;
		private int _tileWidth;
		private Button _trade;
		private Button _capture;
		private ArrayList _tilesList;
		private ArrayList _coordinatesList;
		private Point _emptySlot;

        public GamificationViewAdapter(NonAdminDashBoardView context)
        {
            _context = context;
            GetView(0, null, null);
		}

        public override int Count => throw new NotImplementedException();

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            throw new NotImplementedException();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            _view = _context.LayoutInflater.Inflate(Resource.Layout.GamificationLayout, null);
			findAllElements(_view);
			setGameView();
			makeTilesMethod();
			randomizeMethod();
			return _view;
        }

        public View GetView()
        {
            return _view;
        }

		protected void findAllElements(View view)
		{
			_puzzleLayout = view.FindViewById<GridLayout>(Resource.Id.puzzleGridLayout);
			_trade = view.FindViewById<Button>(Resource.Id.Trade);
			_capture = view.FindViewById<Button>(Resource.Id.Capture);
		}

		private void setGameView()
		{
			_capture.Text = StringConstants.Localizable.CaptureButton;
			_trade.Text = StringConstants.Localizable.TradeButton;
			_gameViewWidth = Resources.System.DisplayMetrics.WidthPixels;
			_puzzleLayout.ColumnCount = 3;
			_puzzleLayout.RowCount = 3;

			_puzzleLayout.LayoutParameters = new LinearLayout.LayoutParams(_gameViewWidth, _gameViewWidth);
		}

		private void makeTilesMethod()
		{
			_tileWidth = _gameViewWidth / 3;

			int counter = 1;
			string imgsource = "piece" + counter;
			string fullsource = "Resource.Drawable." + imgsource; 
			_tilesList = new ArrayList();
			_coordinatesList = new ArrayList();

			for (int row = 0; row < 3; ++row)
			{
				for (int column = 0; column < 3; ++column)
				{
					MyImageView ImageTile = new MyImageView(_context);

					GridLayout.Spec rowSpec = GridLayout.InvokeSpec(row);
					GridLayout.Spec colSpec = GridLayout.InvokeSpec(column);

					GridLayout.LayoutParams tileLayoutParams = new GridLayout.LayoutParams(rowSpec, colSpec);

					//ImageTile.Text = counter.ToString();
					//ImageTile.SetTextColor(Color.Black);
					//ImageTile.TextSize = 40;
					//ImageTile.Gravity = GravityFlags.Center;

					if (counter ==1)
					{
						ImageTile.SetImageResource(Resource.Drawable.piece1);
					}
					else if (counter == 2)
					{
						ImageTile.SetImageResource(Resource.Drawable.piece2);
					}
					else if (counter == 3)
					{
						ImageTile.SetImageResource(Resource.Drawable.piece3);
					}
					else if (counter == 4)
					{
						ImageTile.SetImageResource(Resource.Drawable.piece4);
					}
					else if (counter == 5)
					{
						ImageTile.SetImageResource(Resource.Drawable.piece5);
					}
					else if (counter == 6)
					{
						ImageTile.SetImageResource(Resource.Drawable.piece6);
					}
					else if (counter == 7)
					{
						ImageTile.SetImageResource(Resource.Drawable.piece7);
					}
					else if (counter == 8)
					{
						ImageTile.SetImageResource(Resource.Drawable.piece8);
					}

					tileLayoutParams.Width = _tileWidth - 10;
					tileLayoutParams.Height = _tileWidth - 10;
					tileLayoutParams.SetMargins(5, 5, 5, 5);

					ImageTile.LayoutParameters = tileLayoutParams;
					//textTile.SetBackgroundColor(Color.Green);

					Point thisLocation = new Point(column, row);
					_coordinatesList.Add(thisLocation);
					_tilesList.Add(ImageTile);

					ImageTile.xPos = thisLocation.X;
					ImageTile.yPos = thisLocation.Y;

					ImageTile.Touch += TextTile_Touch;

					_puzzleLayout.AddView(ImageTile);

					counter += 1;
				}
			}
			_puzzleLayout.RemoveView((MyImageView)_tilesList[8]);
			_tilesList.RemoveAt(8);
		}

		private void randomizeMethod()
		{
			Random myRand = new Random();
			ArrayList copyCoordsList = new ArrayList(_coordinatesList);

			foreach (MyImageView any in _tilesList)
			{
				int randIndex = myRand.Next(0, copyCoordsList.Count);
				Point thisRandLoc = (Point)copyCoordsList[randIndex];

				GridLayout.Spec rowSpec = GridLayout.InvokeSpec(thisRandLoc.Y);
				GridLayout.Spec colSpec = GridLayout.InvokeSpec(thisRandLoc.X);
				GridLayout.LayoutParams randLayoutParams = new GridLayout.LayoutParams(rowSpec, colSpec);

				any.xPos = thisRandLoc.X;
				any.yPos = thisRandLoc.Y;

				randLayoutParams.Width = _tileWidth - 10;
				randLayoutParams.Height = _tileWidth - 10;
				randLayoutParams.SetMargins(5, 5, 5, 5);

				any.LayoutParameters = randLayoutParams;
				copyCoordsList.RemoveAt(randIndex);
			
			}
			_emptySlot = (Point)copyCoordsList[0];
		}

		public void TextTile_Touch(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Up) 
			{
				MyImageView thisTile = (MyImageView)sender;
				Console.WriteLine("\r tile is at: \r x={0} \r y={1}", thisTile.xPos, thisTile.yPos);

				float xDif = (float)System.Math.Pow(thisTile.xPos - _emptySlot.X, 2);
				float yDif = (float)System.Math.Pow(thisTile.yPos - _emptySlot.Y, 2);
				float dist = (float)System.Math.Sqrt(xDif + yDif);

				if (dist == 1)
				{
					Point curPoint = new Point(thisTile.xPos, thisTile.yPos);

					GridLayout.Spec rowSpec = GridLayout.InvokeSpec(_emptySlot.Y);
					GridLayout.Spec colSpec = GridLayout.InvokeSpec(_emptySlot.X);

					GridLayout.LayoutParams newLocParams = new GridLayout.LayoutParams(rowSpec, colSpec);

					thisTile.xPos = _emptySlot.X;
					thisTile.yPos = _emptySlot.Y;

					newLocParams.Width = _tileWidth - 10;
					newLocParams.Height = _tileWidth - 10;
					newLocParams.SetMargins(5, 5, 5, 5);

					thisTile.LayoutParameters = newLocParams;
					_emptySlot = curPoint;
				}
			}
		}

		class MyImageView : ImageView
		{
			private Activity _context;

			public MyImageView(Activity context) : base(context)
			{
				_context = context;
			}

			public int xPos { set; get; }
			public int yPos { set; get; }
		}
	}	

}