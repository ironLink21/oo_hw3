using System;
using System.Collections.Generic;

namespace AppLayer.DrawingComponents
{
    public class StarFactory
    {
        public string ResourceNamePattern { get; set; }
        public Type ReferenceType { get; set; }

        private readonly Dictionary<string, StarWithIntrinsicState> _sharedStars = new Dictionary<string, StarWithIntrinsicState>();

        public StarWithAllState GetStar(StarExtrinsicState extrinsicState)
        {
            string resourceName = string.Format(ResourceNamePattern, extrinsicState.StarType);

            StarWithIntrinsicState starWithIntrinsicState;
            if (_sharedStars.ContainsKey(extrinsicState.StarType))
            {
                starWithIntrinsicState = _sharedStars[extrinsicState.StarType];
            }
            else
            {
                starWithIntrinsicState = new StarWithIntrinsicState();
                starWithIntrinsicState.LoadFromResource(resourceName, ReferenceType);
                _sharedStars.Add(extrinsicState.StarType, starWithIntrinsicState);
            }

            return new StarWithAllState(starWithIntrinsicState, extrinsicState);
        }
    }
}
