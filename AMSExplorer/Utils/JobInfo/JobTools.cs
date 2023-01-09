using Azure.ResourceManager.Media.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AMSExplorer.Utils.JobInfo
{
    public class JobTools
    {

        public static async Task<StringBuilder> GetStatAsync(JobExtension MyJobExt, AMSClientV3 _amsClient)
        {
            ListRepData infoStr = new();
            Job MyJob = MyJobExt.Job;

            infoStr.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            infoStr.Add(string.Empty);

            infoStr.Add("Job Name", MyJob.Name);
            infoStr.Add("Based on transform", MyJobExt.TransformName);
            infoStr.Add("Description", MyJob.Description);
            infoStr.Add("Id", MyJob.Id);
            infoStr.Add("Created (UTC)", MyJob.Created.ToLongDateString() + " " + MyJob.Created.ToLongTimeString());
            infoStr.Add("Last Modified (UTC)", MyJob.LastModified.ToLongDateString() + " " + MyJob.LastModified.ToLongTimeString());
            infoStr.Add("Priority", MyJob.Priority);
            infoStr.Add("State", MyJob.State.ToString());

            infoStr.Add(string.Empty);

            if (MyJob.Input is MediaJobInputSequence dd) // multiple inputs
            {
                foreach (var input in dd.Inputs)
                {
                    infoStr.Add("   --- Job Input (Sequence) -------------------------------------");

                    if (input is JobInputAsset iAsset)
                    {
                        infoStr.Add("   Input type", "asset");
                        infoStr.Add("   Asset name", iAsset.AssetName);
                    }
                    else
                    {
                        infoStr.Add("   Input type", "clip");
                    }
                    infoStr.Add("   Input label", input.Label);

                    if (input.Start != null && input.Start is AbsoluteClipTime startA)
                    {
                        infoStr.Add("   Absolute Clip Time Start", startA.Time.ToString());
                    }
                    if (input.End != null && input.End is AbsoluteClipTime endA)
                    {
                        infoStr.Add("   Absolute Clip Time End", endA.Time.ToString());
                    }

                    infoStr.Add("   Files", string.Join(Constants.endline, input.Files));

                    foreach (var idef in input.InputDefinitions)
                    {
                        infoStr.Add("   Input definition", idef.ToString());
                    }
                    infoStr.Add(string.Empty);
                }
            }
            else if (MyJob.Input is JobInputAsset inputA)
            {
                infoStr.Add("   --- Job Input (Single asset) ---------------------------------");

                infoStr.Add("   Asset name", inputA.AssetName);
                infoStr.Add("   Asset type", (await AssetTools.GetAssetTypeAsync(inputA, _amsClient))?.Type);

                if (inputA.Start != null && inputA.Start is AbsoluteClipTime startA)
                {
                    infoStr.Add("   Absolute Clip Time Start", startA.Time.ToString());
                }
                if (inputA.End != null && inputA.End is AbsoluteClipTime endA)
                {
                    infoStr.Add("   Absolute Clip Time End", endA.Time.ToString());
                }
                infoStr.Add("   Input label", inputA.Label);
                infoStr.Add("   Files", string.Join(Constants.endline, inputA.Files));
                infoStr.Add(string.Empty);

            }
            else if (MyJob.Input is JobInputHttp inputH)
            {
                infoStr.Add("   --- Job Input (Https) ----------------------------------------");

                infoStr.Add("   Base Url", inputH.BaseUri);

                if (inputH.Start != null && inputH.Start is AbsoluteClipTime startA)
                {
                    infoStr.Add("   Absolute Clip Time Start", startA.Time.ToString());
                }
                if (inputH.End != null && inputH.End is AbsoluteClipTime endA)
                {
                    infoStr.Add("   Absolute Clip Time End", endA.Time.ToString());
                }
                infoStr.Add("   Input label", inputH.Label);
                infoStr.Add("   Files", string.Join(Constants.endline, inputH.Files));
                infoStr.Add(string.Empty);
            }

            infoStr.Add(string.Empty);

            foreach (var output in MyJob.Outputs)
            {
                infoStr.Add("   --- Job Output -----------------------------------------------");
                infoStr.Add("   Output label", output.Label);
                infoStr.Add("   Output progress", output.Progress.ToString());
                infoStr.Add("   Output state", output.State.ToString());

                if (output.StartTime != null)
                    infoStr.Add("   Output start time", ((DateTime)output.StartTime).ToLongDateString() + " " + ((DateTime)output.StartTime).ToLongTimeString());
                if (output.EndTime != null)
                    infoStr.Add("   Output end time", ((DateTime)output.EndTime).ToLongDateString() + " " + ((DateTime)output.EndTime).ToLongTimeString());

                if (output.Error != null && output.Error.Details != null)
                {
                    for (int i = 0; i < output.Error.Details.Count; i++)
                    {
                        infoStr.Add("   Error code", output.Error.Details[i].Code);
                        infoStr.Add("   Error message", output.Error.Details[i].Message);
                    }
                }
                infoStr.Add(string.Empty);
            }

            infoStr.Add(string.Empty);
            infoStr.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            return infoStr.ReturnStringBuilder();
        }
    }
}