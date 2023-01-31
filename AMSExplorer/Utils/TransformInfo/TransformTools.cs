using AMSExplorer.Rest;
using Azure.ResourceManager.Media;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace AMSExplorer.Utils.TransformInfo
{
    public class TransformTools
    {
        public static StringBuilder GetStat(MediaTransformData transform, TransformRestObject transformRest)
        {
            ListRepData infoStr = new();

            infoStr.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            infoStr.Add(string.Empty);
            infoStr.Add("Transform name", transform.Name);
            infoStr.Add("Description", transform.Description);
            infoStr.Add("Id", transform.Id);
            infoStr.Add("Created (UTC)", transform.CreatedOn?.UtcDateTime.ToString());
            infoStr.Add("Last Modified (UTC)", transform.LastModifiedOn?.UtcDateTime.ToString());
            infoStr.Add(string.Empty);

            if (transform.Outputs.Count > 0)
            {
                int indexO = 0;
                foreach (var output in transform.Outputs)
                {
                    infoStr.Add("   --- Transform Output -----------------------------------------");

                    infoStr.Add("   Preset type", output.Preset.GetType().ToString());
                    infoStr.Add("   Relative priority", output.RelativePriority.ToString());
                    var presetRest = transformRest.Properties.Outputs.Skip(indexO).Take(1).FirstOrDefault().Preset;
                    string presetJson = JsonConvert.SerializeObject(presetRest, Formatting.None);

                    infoStr.Add("   Preset (json)", presetJson);
                    infoStr.Add(string.Empty);

                    indexO++;
                }
            }

            infoStr.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            return infoStr.ReturnStringBuilder();
        }
    }
}
