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
            var MyJob = MyJobExt.Job;

            infoStr.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            infoStr.Add(string.Empty);

            infoStr.Add("Job Name", MyJob.Data.Name);
            infoStr.Add("Based on transform", MyJobExt.TransformName);
            infoStr.Add("Description", MyJob.Data.Description);
            infoStr.Add("Id", MyJob.Data.Id);
            infoStr.Add("Created (UTC)", MyJob.Data.CreatedOn.ToString());
            infoStr.Add("Last Modified (UTC)", MyJob.Data.LastModifiedOn.ToString());
            infoStr.Add("Priority", MyJob.Data.Priority.ToString());
            infoStr.Add("State", MyJob.Data.State.ToString());

            infoStr.Add(string.Empty);

            if (MyJob.Data.Input is MediaJobInputSequence dd) // multiple inputs
            {
                foreach (var input in dd.Inputs)
                {
                    infoStr.Add("   --- Job Input (Sequence) -------------------------------------");

                    if (input is MediaJobInputAsset iAsset)
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
            else if (MyJob.Data.Input is MediaJobInputAsset inputA)
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
            else if (MyJob.Data.Input is MediaJobInputHttp inputH)
            {
                infoStr.Add("   --- Job Input (Https) ----------------------------------------");

                infoStr.Add("   Base Url", inputH.BaseUri.ToString());

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

            foreach (var output in MyJob.Data.Outputs)
            {
                infoStr.Add("   --- Job Output -----------------------------------------------");
                infoStr.Add("   Output label", output.Label);
                infoStr.Add("   Output progress", output.Progress.ToString());
                infoStr.Add("   Output state", output.State.ToString());

                if (output.StartOn != null)
                    infoStr.Add("   Output start time", output.StartOn?.ToString());
                if (output.EndOn != null)
                    infoStr.Add("   Output end time", output.EndOn?.ToString());

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