﻿//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//---------------------------------------------------------------------------------------------

using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using System.Drawing;


namespace AMSExplorer
{

    class LiveEventUtil
    {

        private static readonly Bitmap StandardEncodingImage = Bitmaps.encoding;
        private static readonly Bitmap PremiumEncodingImage = Bitmaps.encodingPremium;
        private static readonly Bitmap StandardPassThroughImage = Bitmaps.passthrough_std;
        private static readonly Bitmap BasicPassThroughImage = Bitmaps.passthrough_basic;

        public static Bitmap ReturnChannelBitmap(MediaLiveEventResource levent)
        {
            return levent.Data.Encoding.EncodingType.ToString() switch
            {
                nameof(LiveEventEncodingType.PassthroughBasic) => BasicPassThroughImage,
                nameof(LiveEventEncodingType.PassthroughStandard) => StandardPassThroughImage,
                nameof(LiveEventEncodingType.Standard) => StandardEncodingImage,
                nameof(LiveEventEncodingType.Premium1080P) => PremiumEncodingImage,
                _ => null,
            };
        }
    }
}
