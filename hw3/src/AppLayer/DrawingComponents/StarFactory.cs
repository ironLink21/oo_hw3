using System;
using System.Collections.Generic;

namespace AppLayer.DrawingComponents
{
    public class StarFactory
    {
        public string ResourceNamePattern { get; set; }
        public Type ReferenceType { get; set; }
        public string StarType { get; set; }

        private readonly Dictionary<string, StarWithIntrinsicState> _sharedStars = new Dictionary<string, StarWithIntrinsicState>();

        public StarWithAllState GetStar(StarExtrinsicState extrinsicState)
        {
            // TODO: this function is having a problem I don't know where 
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
