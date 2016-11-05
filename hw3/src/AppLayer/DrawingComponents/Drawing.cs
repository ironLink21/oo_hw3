using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;

namespace AppLayer.DrawingComponents
{
    public class Drawing
    {
        private static readonly DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(List<StarExtrinsicState>));

        public static List<Star> _stars = new List<Star>();

        private readonly object _myLock = new object();

        public StarFactory Factory { get; set; }
        public Star SelectedVar { get; set; }
        public bool IsDirty { get; set; }
        public int StarCount => _stars.Count;

        public void Clear()
        {
            lock (_myLock)
            {
                _stars.Clear();
                IsDirty = true;
            }
        }

        public void Add(Star star)
        {
            if (star != null)
            {
                lock (_myLock)
                {
                    _stars.Add(star);
                    IsDirty = true;
                }
            }
        }

        public void RemoveLastStar()
        {
            lock (_myLock)
            {
                int lastIndx = _stars.Count-1;
                _stars.RemoveAt(lastIndx);
                IsDirty = true;
            }
        }

        public void RemoveStar(Star star)
        {
            if (star != null)
            {
                lock (_myLock)
                {
                    if (SelectedVar == star)
                    {
                        SelectedVar = null;
                    }
                    _stars.Remove(star);
                    IsDirty = true;
                }
            }
        }

        public Star FindStarAtPosition(Point location)
        {
            Star result;
            lock (_myLock)
            {
                result = _stars.FindLast(t => location.X >= t.Location.X &&
                                              location.X < t.Location.X + t.Size.Width &&
                                              location.Y >= t.Location.Y &&
                                              location.Y < t.Location.Y + t.Size.Height);
            }
            return result;
        }

        public void DeselectAll()
        {
            lock (_myLock)
            {
                foreach (var t in _stars)
                {
                    t.IsSelected = false;
                }
                IsDirty = true;
            }    
        }

        public void SelectAll()
        {
            lock (_myLock)
            {
                foreach (var t in _stars)
                {
                    t.IsSelected = true;
                }
                IsDirty = true;
            }    
        }

        public List<Star> DeleteAllSelected()
        {
            List<Star> starList;
            lock (_myLock)
            {
                starList = _stars.FindAll(t => t.IsSelected);
                _stars.RemoveAll(t => t.IsSelected);
                IsDirty = true;
            }
            return starList;
        }

        public bool Draw(Graphics graphics)
        {
            bool didARedraw = false;
            lock (_myLock)
            {
                if (IsDirty)
                {
                    graphics.Clear(Color.White);
                    foreach (var t in _stars)
                    {
                        t.Draw(graphics);
                    }
                    IsDirty = false;
                    didARedraw = true;
                }
            }
            return didARedraw;
        }

        public void LoadFromStream(Stream stream)
        {
            var extrinsicStates = JsonSerializer.ReadObject(stream) as List<StarExtrinsicState>;
            if (extrinsicStates == null) return;

            lock (_myLock)
            {
                _stars.Clear();
                foreach (StarExtrinsicState extrinsicState in extrinsicStates)
                {
                    Star star = Factory.GetStar(extrinsicState);
                    _stars.Add(star);
                }
                IsDirty = true;
            }
        }

        public void SaveToStream(Stream stream)
        {
            List<StarExtrinsicState> extrinsicStates = new List<StarExtrinsicState>();
            lock (_myLock)
            {
                foreach (Star star in _stars)
                {
                    StarWithAllState t = star as StarWithAllState;
                    if (t!=null)
                    {
                        extrinsicStates.Add(t.ExtrinsicState);
                    }                    
                }
            }
            JsonSerializer.WriteObject(stream, extrinsicStates);
        }

        public List<Star> GetSelected()
        {
            List<Star> starList = new List<Star>();
            lock (_myLock)
            {
                foreach (Star star in _stars)
                {
                    if (star.IsSelected)
                    {
                        starList.Add(star);
                    }                    
                }
            }
            return starList;
        }

        public List<Star> GetObjects()
        {
            List<Star> starList = new List<Star>();
            lock (_myLock)
            {
                foreach (Star star in _stars)
                {
                    starList.Add(star);                
                }
            }
            return starList;
        }

    }
}
