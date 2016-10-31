<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<kayakDocument version="1.2" xml:space="preserve">
    <components>
        <component>
            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
            <property name="_graphDisplayContents" isNull="true"/>
            <property name="_graphMinDisplaySize" isNull="true"/>
            <property name="_timeBase_local" isNull="true"/>
            <property name="acquireChildLicenses" isNull="true"/>
            <property name="assetPieceNoMetadata" isNull="true"/>
            <property name="clipListXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;clipList&gt;
    &lt;clip&gt;
        &lt;videoSource&gt;
            &lt;mediaFile&gt;
                &lt;file&gt;F:\Imagine Communications\Source Files\MXF_XDCAMHD_1080i29_8xMono_AncData.mxf&lt;/file&gt;
            &lt;/mediaFile&gt;
        &lt;/videoSource&gt;
        &lt;audioSource&gt;
            &lt;mediaFile&gt;
                &lt;file&gt;F:\Imagine Communications\Source Files\MXF_XDCAMHD_1080i29_8xMono_AncData.mxf&lt;/file&gt;
            &lt;/mediaFile&gt;
        &lt;/audioSource&gt;
    &lt;/clip&gt;
    &lt;primaryClipIndex&gt;0&lt;/primaryClipIndex&gt;
&lt;/clipList&gt;
</property>
            <property name="connectionTimeout">5</property>
            <property name="defaultAssetName">DEFAULT</property>
            <property name="defaultInputPin" isNull="true"/>
            <property name="defaultOutputPin" isNull="true"/>
            <property name="ignoreChildComponentErrors" isNull="true"/>
            <property name="ignoreParentGraphState" isNull="true"/>
            <property name="inactiveTimeout">600</property>
            <property name="logsMaxEntries" isNull="true"/>
            <property name="monitorProgress">true</property>
            <property name="outputWriteDirectory">H:\Imagine Communications\Output</property>
            <property name="primarySourceFile">F:\Imagine Communications\Source Files\MXF_XDCAMHD_1080i29_8xMono_AncData.mxf</property>
            <property name="submitTimeout">10</property>
            <property name="tmGroup" isNull="true"/>
            <property name="tmHost" isNull="true"/>
            <property name="tmMoveToPath" isNull="true"/>
            <property name="tmPassword" isNull="true"/>
            <property name="tmPriority" isNull="true"/>
            <property name="tmServerPort" isNull="true"/>
            <property name="tmUsername" isNull="true"/>
            <property name="tmWriteToPath" isNull="true"/>
            <property name="transcodeRequestXml" isNull="true"/>
            <componentName>Transcode Blueprint</componentName>
            <componentDefinitionName>Transcode Task Graph</componentDefinitionName>
            <componentDefinitionGuid>cc2f8f8a-85a3-4522-85a5-b0b26b12f4cd</componentDefinitionGuid>
            <componentOwningPluginName>Media Manager</componentOwningPluginName>
            <componentOwningPluginId>ca.digitalrapids.MediaManager</componentOwningPluginId>
            <childComponents>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="Duration">Depend on the shortest source</property>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">551.0,211.0</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">out</property>
                    <property name="framesPerPacket">1024</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio Stream Interleaver</componentName>
                    <componentDefinitionName>Audio Stream Interleaver</componentDefinitionName>
                    <componentDefinitionGuid>D166D48B-FA26-44ca-8F2D-62B20D892659</componentDefinitionGuid>
                    <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                    <childComponents/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                    <pin name="done" type="EVENT"/>
                    <pin name="in 1" type="INPUT_PUSH">
                        <pinDefinition name="in 1" displayName="Raw Audio 1" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="in 2" type="INPUT_PUSH">
                        <pinDefinition name="in 2" displayName="Raw Audio 2" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="in 3" type="INPUT_PUSH">
                        <pinDefinition name="in 3" displayName="Raw Audio 3" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="in 4" type="INPUT_PUSH">
                        <pinDefinition name="in 4" displayName="Raw Audio 4" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="in 5" type="INPUT_PUSH">
                        <pinDefinition name="in 5" displayName="Raw Audio 5" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="in 6" type="INPUT_PUSH">
                        <pinDefinition name="in 6" displayName="Raw Audio 6" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="in 7" type="INPUT_PUSH">
                        <pinDefinition name="in 7" displayName="Raw Audio 7" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">792.0,88.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Missing Metadata Update and Deinterlacing</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">30.0,10.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize </componentName>
                            <componentDefinitionName>Kayak Graph</componentDefinitionName>
                            <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                            <componentOwningPluginName>KayakCore</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                            <childComponents>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">620.9619140625,93.83748245239258</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="defaultInputPin">in1</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Branch Merger</componentName>
                                    <componentDefinitionName>BranchMerger</componentDefinitionName>
                                    <componentDefinitionGuid>5442b6d8-5672-430f-91ce-5b047e84bd7f</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in1" type="INPUT_PUSH"/>
                                    <pin name="out" type="OUTPUT_PUSH"/>
                                    <pin name="in2" type="INPUT_PUSH">
                                        <pinDefinition name="in2" displayName="In 2" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                    <pin name="in3" type="INPUT_PUSH">
                                        <pinDefinition name="in3" displayName="In 3" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents">false</property>
                                    <property name="_graphDisplayLocation">219.68359375,0.0</property>
                                    <property name="_graphMinDisplaySize" isNull="true"/>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="accuracy_bits_per_sample" isNull="true"/>
                                    <property name="acquireChildLicenses" isNull="true"/>
                                    <property name="average_bit_rate" isNull="true"/>
                                    <property name="bits_per_sample" isNull="true"/>
                                    <property name="color_space_standard">rec709</property>
                                    <property name="custom_aspect_ratio">16/9</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="display_aspect_ratio">16/9</property>
                                    <property name="display_aspect_ratio_shadow">16/9</property>
                                    <property name="field_dominance" isNull="true"/>
                                    <property name="frame_layout">progressive</property>
                                    <property name="frame_rate" isNull="true"/>
                                    <property name="frame_rate_shadow" isNull="true"/>
                                    <property name="ignoreChildComponentErrors" isNull="true"/>
                                    <property name="ignoreParentGraphState" isNull="true"/>
                                    <property name="language_code" isNull="true"/>
                                    <property name="language_code_shadow" isNull="true"/>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <property name="maximum_bit_rate" isNull="true"/>
                                    <property name="out_endian" isNull="true"/>
                                    <property name="override_input">FALSE</property>
                                    <property name="pulldown_mode" isNull="true"/>
                                    <property name="rate_control" isNull="true"/>
                                    <property name="sample_signed" isNull="true"/>
                                    <property name="sd_no_overscan" isNull="true"/>
                                    <property name="sd_no_overscan_shadow" isNull="true"/>
                                    <property name="segmented_frame" isNull="true"/>
                                    <property name="storage_bits_per_sample" isNull="true"/>
                                    <componentName>Video Data Type Updater - assume HD</componentName>
                                    <componentDefinitionName>Video Data Type Updater</componentDefinitionName>
                                    <componentDefinitionGuid>D7576695-6BCB-410F-BB86-734E5F526924</componentDefinitionGuid>
                                    <componentOwningPluginName>VideoFormatUtilities</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.VideoFormatUtilities</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_IO"/>
                                    <pin name="out" type="OUTPUT_IO"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents">false</property>
                                    <property name="_graphDisplayLocation">230.275146484375,161.4857177734375</property>
                                    <property name="_graphMinDisplaySize" isNull="true"/>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="accuracy_bits_per_sample" isNull="true"/>
                                    <property name="acquireChildLicenses" isNull="true"/>
                                    <property name="average_bit_rate" isNull="true"/>
                                    <property name="bits_per_sample" isNull="true"/>
                                    <property name="color_space_standard">rec601</property>
                                    <property name="custom_aspect_ratio">16/9</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="display_aspect_ratio">4/3</property>
                                    <property name="display_aspect_ratio_shadow">4/3</property>
                                    <property name="field_dominance" isNull="true"/>
                                    <property name="frame_layout">progressive</property>
                                    <property name="frame_rate" isNull="true"/>
                                    <property name="frame_rate_shadow" isNull="true"/>
                                    <property name="ignoreChildComponentErrors" isNull="true"/>
                                    <property name="ignoreParentGraphState" isNull="true"/>
                                    <property name="language_code" isNull="true"/>
                                    <property name="language_code_shadow" isNull="true"/>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <property name="maximum_bit_rate" isNull="true"/>
                                    <property name="out_endian" isNull="true"/>
                                    <property name="override_input">FALSE</property>
                                    <property name="pulldown_mode" isNull="true"/>
                                    <property name="rate_control" isNull="true"/>
                                    <property name="sample_signed" isNull="true"/>
                                    <property name="sd_no_overscan" isNull="true"/>
                                    <property name="sd_no_overscan_shadow" isNull="true"/>
                                    <property name="segmented_frame" isNull="true"/>
                                    <property name="storage_bits_per_sample" isNull="true"/>
                                    <componentName>Video Data Type Updater - assume SD</componentName>
                                    <componentDefinitionName>Video Data Type Updater</componentDefinitionName>
                                    <componentDefinitionGuid>D7576695-6BCB-410F-BB86-734E5F526924</componentDefinitionGuid>
                                    <componentOwningPluginName>VideoFormatUtilities</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.VideoFormatUtilities</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_IO"/>
                                    <pin name="out" type="OUTPUT_IO"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">0.0,44.784812927246094</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;GREATER_THAN_OR_EQUAL&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_height"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="720"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin" isNull="true"/>
                                    <property name="dynamicSwitching" isNull="true"/>
                                    <property name="evaluateEndOfStream">false</property>
                                    <property name="logicOperator">and</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Branch</componentName>
                                    <componentDefinitionName>Logic Branch</componentDefinitionName>
                                    <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="yes" type="OUTPUT_PUSH"/>
                                    <pin name="no" type="OUTPUT_PUSH"/>
                                </component>
                            </childComponents>
                            <pin name="out" type="OUTPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">584.0,569.0</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="out" displayName="Out" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="in" type="INPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">584.0,569.0</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="in" displayName="In" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">472.076171875,8.9891357421875</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize </componentName>
                            <componentDefinitionName>Kayak Graph</componentDefinitionName>
                            <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                            <componentOwningPluginName>KayakCore</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                            <childComponents>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents">false</property>
                                    <property name="_graphDisplayLocation">1045.85546875,140.78182983398438</property>
                                    <property name="_graphMinDisplaySize" isNull="true"/>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="accuracy_bits_per_sample" isNull="true"/>
                                    <property name="acquireChildLicenses" isNull="true"/>
                                    <property name="average_bit_rate" isNull="true"/>
                                    <property name="bits_per_sample" isNull="true"/>
                                    <property name="color_space_standard" isNull="true"/>
                                    <property name="custom_aspect_ratio">16/9</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="display_aspect_ratio" isNull="true"/>
                                    <property name="display_aspect_ratio_shadow" isNull="true"/>
                                    <property name="field_dominance">bottom_field</property>
                                    <property name="frame_layout">interlaced</property>
                                    <property name="frame_rate" isNull="true"/>
                                    <property name="frame_rate_shadow" isNull="true"/>
                                    <property name="ignoreChildComponentErrors" isNull="true"/>
                                    <property name="ignoreParentGraphState" isNull="true"/>
                                    <property name="language_code" isNull="true"/>
                                    <property name="language_code_shadow" isNull="true"/>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <property name="maximum_bit_rate" isNull="true"/>
                                    <property name="out_endian" isNull="true"/>
                                    <property name="override_input">FALSE</property>
                                    <property name="pulldown_mode" isNull="true"/>
                                    <property name="rate_control" isNull="true"/>
                                    <property name="sample_signed" isNull="true"/>
                                    <property name="sd_no_overscan" isNull="true"/>
                                    <property name="sd_no_overscan_shadow" isNull="true"/>
                                    <property name="segmented_frame" isNull="true"/>
                                    <property name="storage_bits_per_sample" isNull="true"/>
                                    <componentName>Video Data Type Updater - Assume Interlaced BFF</componentName>
                                    <componentDefinitionName>Video Data Type Updater</componentDefinitionName>
                                    <componentDefinitionGuid>D7576695-6BCB-410F-BB86-734E5F526924</componentDefinitionGuid>
                                    <componentOwningPluginName>VideoFormatUtilities</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.VideoFormatUtilities</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_IO"/>
                                    <pin name="out" type="OUTPUT_IO"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents">false</property>
                                    <property name="_graphDisplayLocation">1051.2991943359375,276.5455322265625</property>
                                    <property name="_graphMinDisplaySize" isNull="true"/>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="accuracy_bits_per_sample" isNull="true"/>
                                    <property name="acquireChildLicenses" isNull="true"/>
                                    <property name="average_bit_rate" isNull="true"/>
                                    <property name="bits_per_sample" isNull="true"/>
                                    <property name="color_space_standard" isNull="true"/>
                                    <property name="custom_aspect_ratio">16/9</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="display_aspect_ratio" isNull="true"/>
                                    <property name="display_aspect_ratio_shadow" isNull="true"/>
                                    <property name="field_dominance">top_field</property>
                                    <property name="frame_layout">interlaced</property>
                                    <property name="frame_rate" isNull="true"/>
                                    <property name="frame_rate_shadow" isNull="true"/>
                                    <property name="ignoreChildComponentErrors" isNull="true"/>
                                    <property name="ignoreParentGraphState" isNull="true"/>
                                    <property name="language_code" isNull="true"/>
                                    <property name="language_code_shadow" isNull="true"/>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <property name="maximum_bit_rate" isNull="true"/>
                                    <property name="out_endian" isNull="true"/>
                                    <property name="override_input">FALSE</property>
                                    <property name="pulldown_mode" isNull="true"/>
                                    <property name="rate_control" isNull="true"/>
                                    <property name="sample_signed" isNull="true"/>
                                    <property name="sd_no_overscan" isNull="true"/>
                                    <property name="sd_no_overscan_shadow" isNull="true"/>
                                    <property name="segmented_frame" isNull="true"/>
                                    <property name="storage_bits_per_sample" isNull="true"/>
                                    <componentName>Video Data Type Updater - Assume Interlaced TFF</componentName>
                                    <componentDefinitionName>Video Data Type Updater</componentDefinitionName>
                                    <componentDefinitionGuid>D7576695-6BCB-410F-BB86-734E5F526924</componentDefinitionGuid>
                                    <componentOwningPluginName>VideoFormatUtilities</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.VideoFormatUtilities</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_IO"/>
                                    <pin name="out" type="OUTPUT_IO"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">1520.25927734375,81.99114990234375</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="defaultInputPin">in1</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Branch Merger 3</componentName>
                                    <componentDefinitionName>BranchMerger</componentDefinitionName>
                                    <componentDefinitionGuid>5442b6d8-5672-430f-91ce-5b047e84bd7f</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in1" type="INPUT_PUSH"/>
                                    <pin name="out" type="OUTPUT_PUSH"/>
                                    <pin name="in2" type="INPUT_PUSH">
                                        <pinDefinition name="in2" displayName="In 2" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                    <pin name="in3" type="INPUT_PUSH">
                                        <pinDefinition name="in3" displayName="In 3" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                    <pin name="in4" type="INPUT_PUSH">
                                        <pinDefinition name="in4" displayName="In 4" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents">false</property>
                                    <property name="_graphDisplayLocation">1035.3349609375,20.217559814453125</property>
                                    <property name="_graphMinDisplaySize" isNull="true"/>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="accuracy_bits_per_sample" isNull="true"/>
                                    <property name="acquireChildLicenses" isNull="true"/>
                                    <property name="average_bit_rate" isNull="true"/>
                                    <property name="bits_per_sample" isNull="true"/>
                                    <property name="color_space_standard" isNull="true"/>
                                    <property name="custom_aspect_ratio">16/9</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="display_aspect_ratio" isNull="true"/>
                                    <property name="display_aspect_ratio_shadow" isNull="true"/>
                                    <property name="field_dominance" isNull="true"/>
                                    <property name="frame_layout">progressive</property>
                                    <property name="frame_rate" isNull="true"/>
                                    <property name="frame_rate_shadow" isNull="true"/>
                                    <property name="ignoreChildComponentErrors" isNull="true"/>
                                    <property name="ignoreParentGraphState" isNull="true"/>
                                    <property name="language_code" isNull="true"/>
                                    <property name="language_code_shadow" isNull="true"/>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <property name="maximum_bit_rate" isNull="true"/>
                                    <property name="out_endian" isNull="true"/>
                                    <property name="override_input">FALSE</property>
                                    <property name="pulldown_mode" isNull="true"/>
                                    <property name="rate_control" isNull="true"/>
                                    <property name="sample_signed" isNull="true"/>
                                    <property name="sd_no_overscan" isNull="true"/>
                                    <property name="sd_no_overscan_shadow" isNull="true"/>
                                    <property name="segmented_frame" isNull="true"/>
                                    <property name="storage_bits_per_sample" isNull="true"/>
                                    <componentName>Video Data Type Updater - Assume Progressive</componentName>
                                    <componentDefinitionName>Video Data Type Updater</componentDefinitionName>
                                    <componentDefinitionGuid>D7576695-6BCB-410F-BB86-734E5F526924</componentDefinitionGuid>
                                    <componentOwningPluginName>VideoFormatUtilities</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.VideoFormatUtilities</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_IO"/>
                                    <pin name="out" type="OUTPUT_IO"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">753.9630126953125,19.31671142578125</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="defaultInputPin">in1</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Branch Merger</componentName>
                                    <componentDefinitionName>BranchMerger</componentDefinitionName>
                                    <componentDefinitionGuid>5442b6d8-5672-430f-91ce-5b047e84bd7f</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in1" type="INPUT_PUSH"/>
                                    <pin name="out" type="OUTPUT_PUSH"/>
                                    <pin name="in2" type="INPUT_PUSH">
                                        <pinDefinition name="in2" displayName="In 2" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                    <pin name="in3" type="INPUT_PUSH">
                                        <pinDefinition name="in3" displayName="In 3" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">383.6317138671875,145.35000610351562</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_height"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="720"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin" isNull="true"/>
                                    <property name="dynamicSwitching" isNull="true"/>
                                    <property name="evaluateEndOfStream">false</property>
                                    <property name="logicOperator">and</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Branch 4</componentName>
                                    <componentDefinitionName>Logic Branch</componentDefinitionName>
                                    <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="yes" type="OUTPUT_PUSH"/>
                                    <pin name="no" type="OUTPUT_PUSH"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">594.244384765625,287.19940185546875</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="defaultInputPin">in1</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Branch Merger 2</componentName>
                                    <componentDefinitionName>BranchMerger</componentDefinitionName>
                                    <componentDefinitionGuid>5442b6d8-5672-430f-91ce-5b047e84bd7f</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in1" type="INPUT_PUSH"/>
                                    <pin name="out" type="OUTPUT_PUSH"/>
                                    <pin name="in2" type="INPUT_PUSH">
                                        <pinDefinition name="in2" displayName="In 2" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                    <pin name="in3" type="INPUT_PUSH">
                                        <pinDefinition name="in3" displayName="In 3" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">795.4869384765625,205.4410400390625</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="frame_rate"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="30000/1001"/&gt;
        &lt;/Condition&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="frame_rate"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="30"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin" isNull="true"/>
                                    <property name="dynamicSwitching" isNull="true"/>
                                    <property name="evaluateEndOfStream">false</property>
                                    <property name="logicOperator">or</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Branch</componentName>
                                    <componentDefinitionName>Logic Branch</componentDefinitionName>
                                    <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="yes" type="OUTPUT_PUSH"/>
                                    <pin name="no" type="OUTPUT_PUSH"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">146.4251708984375,286.1884765625</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="frame_rate"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="60000"/&gt;
        &lt;/Condition&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="frame_rate"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="60000/1001"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin" isNull="true"/>
                                    <property name="dynamicSwitching" isNull="true"/>
                                    <property name="evaluateEndOfStream">false</property>
                                    <property name="logicOperator">or</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Branch 3</componentName>
                                    <componentDefinitionName>Logic Branch</componentDefinitionName>
                                    <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="yes" type="OUTPUT_PUSH"/>
                                    <pin name="no" type="OUTPUT_PUSH"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">0.0,19.43634033203125</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="frame_rate"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="24000/1001"/&gt;
        &lt;/Condition&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="frame_rate"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="24"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin" isNull="true"/>
                                    <property name="dynamicSwitching" isNull="true"/>
                                    <property name="evaluateEndOfStream">false</property>
                                    <property name="logicOperator">or</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Branch 2</componentName>
                                    <componentDefinitionName>Logic Branch</componentDefinitionName>
                                    <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="yes" type="OUTPUT_PUSH"/>
                                    <pin name="no" type="OUTPUT_PUSH"/>
                                </component>
                            </childComponents>
                            <pin name="out" type="OUTPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">530.0,423.0</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="out" displayName="Out" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="in" type="INPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">530.0,423.0</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="in" displayName="In" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">884.0,10.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>LOGIC 
Deinterlace only if necessary</componentName>
                            <componentDefinitionName>Kayak Graph</componentDefinitionName>
                            <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                            <componentOwningPluginName>KayakCore</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                            <childComponents>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">596.419189453125,0.0</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="defaultInputPin">in1</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Branch Merger</componentName>
                                    <componentDefinitionName>BranchMerger</componentDefinitionName>
                                    <componentDefinitionGuid>5442b6d8-5672-430f-91ce-5b047e84bd7f</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in1" type="INPUT_PUSH"/>
                                    <pin name="out" type="OUTPUT_PUSH"/>
                                    <pin name="in2" type="INPUT_PUSH">
                                        <pinDefinition name="in2" displayName="In 2" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                    <pin name="in3" type="INPUT_PUSH">
                                        <pinDefinition name="in3" displayName="In 3" type="INPUT_PUSH" dynamic="true"/>
                                    </pin>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">0.0,15.16314697265625</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="frame_layout"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="progressive"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin" isNull="true"/>
                                    <property name="dynamicSwitching" isNull="true"/>
                                    <property name="evaluateEndOfStream">false</property>
                                    <property name="logicOperator">and</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Branch</componentName>
                                    <componentDefinitionName>Logic Branch</componentDefinitionName>
                                    <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="yes" type="OUTPUT_PUSH"/>
                                    <pin name="no" type="OUTPUT_PUSH"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="output_width_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction minValue="80" minValueExclusive="false"/>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="output_height_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction minValue="80" minValueExclusive="false"/>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="Frame Rate_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue displayName="Pass-through"></enumerationValue>
                                                <enumerationValue val="9.99p" displayName="9.99p"></enumerationValue>
                                                <enumerationValue val="10p" displayName="10p"></enumerationValue>
                                                <enumerationValue val="11.99p" displayName="11.99p"></enumerationValue>
                                                <enumerationValue val="12p" displayName="12p"></enumerationValue>
                                                <enumerationValue val="12.5p" displayName="12.5p"></enumerationValue>
                                                <enumerationValue val="14.99p" displayName="14.99p"></enumerationValue>
                                                <enumerationValue val="15p" displayName="15p"></enumerationValue>
                                                <enumerationValue val="23.98p" displayName="23.98p"></enumerationValue>
                                                <enumerationValue val="24p" displayName="24p"></enumerationValue>
                                                <enumerationValue val="25i" displayName="25i"></enumerationValue>
                                                <enumerationValue val="25p" displayName="25p"></enumerationValue>
                                                <enumerationValue val="29.97i" displayName="29.97i"></enumerationValue>
                                                <enumerationValue val="29.97p" displayName="29.97p"></enumerationValue>
                                                <enumerationValue val="30i" displayName="30i"></enumerationValue>
                                                <enumerationValue val="30p" displayName="30p"></enumerationValue>
                                                <enumerationValue val="50p" displayName="50p"></enumerationValue>
                                                <enumerationValue val="59.94p" displayName="59.94p"></enumerationValue>
                                                <enumerationValue val="60p" displayName="60p"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="Aspect Ratio_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue displayName="Pass-through"></enumerationValue>
                                                <enumerationValue val="16:9" displayName="16:9"></enumerationValue>
                                                <enumerationValue val="4:3" displayName="4:3"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="bits_per_sample_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction minValue="1" minValueExclusive="false" maxValue="16" maxValueExclusive="false"/>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="scanline_alignment_hidden" dynamic="true">
                                        <valueType type="INTEGER"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="plane_alignment_hidden" dynamic="true">
                                        <valueType type="INTEGER"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="preset_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="custom" displayName="Custom"></enumerationValue>
                                                <enumerationValue val="bgr_24" displayName="BGR 24"></enumerationValue>
                                                <enumerationValue val="bgra_32" displayName="BGRA 32"></enumerationValue>
                                                <enumerationValue val="yuyv" displayName="YUYV"></enumerationValue>
                                                <enumerationValue val="uyvy" displayName="UYVY"></enumerationValue>
                                                <enumerationValue val="i420" displayName="i420"></enumerationValue>
                                                <enumerationValue val="v210" displayName="v210"></enumerationValue>
                                                <enumerationValue val="yuv8_422_planar" displayName="YUV 8-bit 422 planar"></enumerationValue>
                                                <enumerationValue val="yuv8_444_planar" displayName="YUV 8-bit 444 planar"></enumerationValue>
                                                <enumerationValue val="yuv16_422_planar" displayName="YUV 16-bit 422 planar"></enumerationValue>
                                                <enumerationValue val="yuv16_444_planar" displayName="YUV 16-bit 444 planar"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="color_space_standard_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue displayName="Pass-through"></enumerationValue>
                                                <enumerationValue val="rec601" displayName="YUV 601"></enumerationValue>
                                                <enumerationValue val="rec709" displayName="YUV 709"></enumerationValue>
                                                <enumerationValue val="yuv601_full_range" displayName="YUV 601 Full Range" description="YUV 601 full range colorspace."></enumerationValue>
                                                <enumerationValue val="yuv709_full_range" displayName="YUV 709 Full Range" description="YUV 709 full range colorspace."></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="mode_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue displayName="Pass-through"></enumerationValue>
                                                <enumerationValue val="auto" displayName="Remove overscan if present"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="filterControl_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="0" displayName="Automatic" description="The number of taps is selected automatically based on the scale ratio."></enumerationValue>
                                                <enumerationValue val="1" displayName="7 Taps" description="7 filter taps are used."></enumerationValue>
                                                <enumerationValue val="2" displayName="15 Taps" description="15 filter taps are used."></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="filterType_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="0" displayName="Imagine Communications" description="Imagine Communications filtering."></enumerationValue>
                                                <enumerationValue val="1" displayName="Lanczos" description="Lanczos filtering."></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="threads_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction minValue="1" minValueExclusive="false" maxValue="16"/>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="verticalBandwidthControl_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="0" displayName="Low" description="The lowest vertical bandwidth."></enumerationValue>
                                                <enumerationValue val="1" displayName="Recommended" description="Recommended vertical bandwidth suitable for most content."></enumerationValue>
                                                <enumerationValue val="2" displayName="High" description="The highest vertical bandwidth which will result in greater vertical resolution but could introduce flickering."></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="latency_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction minValue="3" minValueExclusive="false" maxValue="60"/>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="noiseTolerance_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="0" displayName="Low" description="Low detection noise tolerance. Recommended for clean sources including SDI, DVI, analog component or S-video."></enumerationValue>
                                                <enumerationValue val="1" displayName="Adaptive" description="Adaptive detection noise tolerance. Recommended for noisy sources including composite, analog RF or VCR."></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="detect22Cadence_hidden" dynamic="true">
                                        <valueType type="BOOLEAN"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="detect32Cadence_hidden" dynamic="true">
                                        <valueType type="BOOLEAN"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="detect2332Cadence_hidden" dynamic="true">
                                        <valueType type="BOOLEAN"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="detect2224Cadence_hidden" dynamic="true">
                                        <valueType type="BOOLEAN"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="detect32322Cadence_hidden" dynamic="true">
                                        <valueType type="BOOLEAN"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="detect55Cadence_hidden" dynamic="true">
                                        <valueType type="BOOLEAN"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="detect64Cadence_hidden" dynamic="true">
                                        <valueType type="BOOLEAN"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="detect87Cadence_hidden" dynamic="true">
                                        <valueType type="BOOLEAN"/>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="cadenceReEntryMode_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="0" displayName="Immediate" description="This mode is good when the source generally has a cadence but has occasional discontinuities.  An example is a 60 fields per second source that originated from a telecine process and then the video was subsequently edited.  In this example, the cadence generally breaks at edit points but immediately resumes."></enumerationValue>
                                                <enumerationValue val="1" displayName="Timed Lockout" description="This mode is good for sources that have cadence but have been subsequently time adjusted.  An example is a 60 fields per second source that originated from a telecine process and then the video was subsequently 'time squeezed' to fit more commercials in for broadcast or 'time stretched' in order to fill a particular length time slot.  In these cases, this mode will prevent excessive 'cadence thrashing' where cadence modes are constantly being entered and left."></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="outputFrameRate_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="9_99" displayName="9.99 FPS"></enumerationValue>
                                                <enumerationValue val="10" displayName="10 FPS"></enumerationValue>
                                                <enumerationValue val="11_99" displayName="11.99 FPS"></enumerationValue>
                                                <enumerationValue val="12" displayName="12 FPS"></enumerationValue>
                                                <enumerationValue val="12_5" displayName="12.5 FPS"></enumerationValue>
                                                <enumerationValue val="14_99" displayName="14.99 FPS"></enumerationValue>
                                                <enumerationValue val="15" displayName="15 FPS"></enumerationValue>
                                                <enumerationValue val="23_976" displayName="23.976 FPS"></enumerationValue>
                                                <enumerationValue val="24" displayName="24 FPS"></enumerationValue>
                                                <enumerationValue val="25" displayName="25 FPS"></enumerationValue>
                                                <enumerationValue val="29_97" displayName="29.97 FPS"></enumerationValue>
                                                <enumerationValue val="30" displayName="30 FPS"></enumerationValue>
                                                <enumerationValue val="50" displayName="50 FPS"></enumerationValue>
                                                <enumerationValue val="59_94" displayName="59.94 FPS"></enumerationValue>
                                                <enumerationValue val="60" displayName="60 FPS"></enumerationValue>
                                                <enumerationValue val="matchInputFieldRate" displayName="Match Input Field Rate"></enumerationValue>
                                                <enumerationValue val="matchInputFrameRate" displayName="Match Input Frame Rate"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="sample_layout_strategy_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationFromDataType dataTypeDefinition="PixelFormat" type="sample_layout_strategy"/>
                                                <enumerationValue val="packed" displayName="Packed"></enumerationValue>
                                                <enumerationValue val="planar" displayName="Planar"></enumerationValue>
                                                <enumerationValue val="palette" displayName="Palette"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="sample_layout_details_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationFromDataType dataTypeDefinition="PixelFormat" type="sample_layout_details"/>
                                                <enumerationValue val="bgr" displayName="BGR"></enumerationValue>
                                                <enumerationValue val="bgra" displayName="BGRA"></enumerationValue>
                                                <enumerationValue val="rgb" displayName="RGB"></enumerationValue>
                                                <enumerationValue val="rgba" displayName="RGBA"></enumerationValue>
                                                <enumerationValue val="argb" displayName="ARGB"></enumerationValue>
                                                <enumerationValue val="uyvy" displayName="UYVY"></enumerationValue>
                                                <enumerationValue val="yuyv" displayName="YUYV"></enumerationValue>
                                                <enumerationValue val="uyvy10" displayName="UYVY10"></enumerationValue>
                                                <enumerationValue val="yuyv10" displayName="YUYV10"></enumerationValue>
                                                <enumerationValue val="yuv210" displayName="YUV210"></enumerationValue>
                                                <enumerationValue val="yuv410" displayName="YUV410"></enumerationValue>
                                                <enumerationValue val="yuv" displayName="YUV"></enumerationValue>
                                                <enumerationValue val="yuva" displayName="YUVA"></enumerationValue>
                                                <enumerationValue val="yvu" displayName="YVU"></enumerationValue>
                                                <enumerationValue val="grayscale" displayName="Grayscale"></enumerationValue>
                                                <enumerationValue val="grayscale_alpha" displayName="Grayscale alpha"></enumerationValue>
                                                <enumerationValue val="nexio_yuyv10" displayName="Nexio 10-bit YUV"></enumerationValue>
                                                <enumerationValue val="nexio_yuyv12" displayName="Nexio 12-bit YUV"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="pixel_sampling_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationFromDataType dataTypeDefinition="PixelFormat" type="pixel_sampling"/>
                                                <enumerationValue val="4" displayName="4"></enumerationValue>
                                                <enumerationValue val="444" displayName="444"></enumerationValue>
                                                <enumerationValue val="422" displayName="422"></enumerationValue>
                                                <enumerationValue val="420" displayName="420"></enumerationValue>
                                                <enumerationValue val="411" displayName="411"></enumerationValue>
                                                <enumerationValue val="4444" displayName="4444"></enumerationValue>
                                                <enumerationValue val="4224" displayName="4224"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="storage_bits_per_sample_hidden" dynamic="true">
                                        <valueType type="INTEGER">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="8"></enumerationValue>
                                                <enumerationValue val="16"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="endian_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationFromDataType dataTypeDefinition="PixelFormat" type="endian"/>
                                                <enumerationValue>big</enumerationValue>
                                                <enumerationValue>little</enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="raster_orientation_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationFromDataType dataTypeDefinition="Raster" type="raster_orientation"/>
                                                <enumerationValue val="top_down" displayName="Top down"></enumerationValue>
                                                <enumerationValue val="bottom_up" displayName="Bottom up"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="pulldown_mode_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="2:3TFF" displayName="2:3 TFF"></enumerationValue>
                                                <enumerationValue val="2:3BFF" displayName="2:3 BFF"></enumerationValue>
                                                <enumerationValue val="2:3:3:2" displayName="2:3:3:2"></enumerationValue>
                                                <enumerationValue val="FieldDouble" displayName="FieldDouble"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="dominance_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationFromDataType dataTypeDefinition="VideoInterlacing" type="field_dominance"/>
                                                <enumerationValue val="pass_through" displayName="Pass-through"></enumerationValue>
                                                <enumerationValue val="top_field" displayName="Top field"></enumerationValue>
                                                <enumerationValue val="bottom_field" displayName="Bottom field"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="shift_hidden" dynamic="true">
                                        <valueType type="STRING">
                                            <valueRestriction strictEnum="true">
                                                <enumerationValue val="shift_up" displayName="Shift up"></enumerationValue>
                                                <enumerationValue val="shift_down" displayName="Shift down"></enumerationValue>
                                            </valueRestriction>
                                        </valueType>
                                    </propertyDefinition>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                                    <property name="Aspect Ratio" isNull="true"/>
                                    <property name="Aspect Ratio_hidden" isNull="true"/>
                                    <property name="Frame Rate" isNull="true"/>
                                    <property name="Frame Rate_hidden" isNull="true"/>
                                    <property name="_graphDisplayContents">false</property>
                                    <property name="_graphDisplayLocation">180.9339599609375,146.71157836914062</property>
                                    <property name="_graphMinDisplaySize" isNull="true"/>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="acquireChildLicenses" isNull="true"/>
                                    <property name="bits_per_sample">8</property>
                                    <property name="bits_per_sample_hidden">8</property>
                                    <property name="cadenceReEntryMode">0</property>
                                    <property name="cadenceReEntryMode_hidden">0</property>
                                    <property name="color_space_standard" isNull="true"/>
                                    <property name="color_space_standard_hidden" isNull="true"/>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="detect2224Cadence">true</property>
                                    <property name="detect2224Cadence_hidden">true</property>
                                    <property name="detect22Cadence">true</property>
                                    <property name="detect22Cadence_hidden">true</property>
                                    <property name="detect2332Cadence">true</property>
                                    <property name="detect2332Cadence_hidden">true</property>
                                    <property name="detect32322Cadence">true</property>
                                    <property name="detect32322Cadence_hidden">true</property>
                                    <property name="detect32Cadence">true</property>
                                    <property name="detect32Cadence_hidden">true</property>
                                    <property name="detect55Cadence">true</property>
                                    <property name="detect55Cadence_hidden">true</property>
                                    <property name="detect64Cadence">true</property>
                                    <property name="detect64Cadence_hidden">true</property>
                                    <property name="detect87Cadence">true</property>
                                    <property name="detect87Cadence_hidden">true</property>
                                    <property name="dominance">pass_through</property>
                                    <property name="dominance_hidden">pass_through</property>
                                    <property name="endian" isNull="true"/>
                                    <property name="endian_hidden" isNull="true"/>
                                    <property name="filterControl">0</property>
                                    <property name="filterControl_hidden">0</property>
                                    <property name="filterType">0</property>
                                    <property name="filterType_hidden">0</property>
                                    <property name="ignoreChildComponentErrors" isNull="true"/>
                                    <property name="ignoreParentGraphState" isNull="true"/>
                                    <property name="latency">40</property>
                                    <property name="latency_hidden">40</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <property name="mode" isNull="true"/>
                                    <property name="mode_hidden" isNull="true"/>
                                    <property name="noiseTolerance">0</property>
                                    <property name="noiseTolerance_hidden">0</property>
                                    <property name="outputFrameRate">matchInputFieldRate</property>
                                    <property name="outputFrameRate_hidden">matchInputFieldRate</property>
                                    <property name="output_height" isNull="true"/>
                                    <property name="output_height_hidden" isNull="true"/>
                                    <property name="output_width" isNull="true"/>
                                    <property name="output_width_hidden" isNull="true"/>
                                    <property name="pixel_sampling">422</property>
                                    <property name="pixel_sampling_hidden">422</property>
                                    <property name="plane_alignment">32</property>
                                    <property name="plane_alignment_hidden">32</property>
                                    <property name="preset">yuv8_422_planar</property>
                                    <property name="preset_hidden">yuv8_422_planar</property>
                                    <property name="pulldown_mode">2:3TFF</property>
                                    <property name="pulldown_mode_hidden">2:3TFF</property>
                                    <property name="raster_orientation">top_down</property>
                                    <property name="raster_orientation_hidden">top_down</property>
                                    <property name="sample_layout_details">yuv</property>
                                    <property name="sample_layout_details_hidden">yuv</property>
                                    <property name="sample_layout_strategy">planar</property>
                                    <property name="sample_layout_strategy_hidden">planar</property>
                                    <property name="scanline_alignment">32</property>
                                    <property name="scanline_alignment_hidden">32</property>
                                    <property name="shift">shift_up</property>
                                    <property name="shift_hidden">shift_up</property>
                                    <property name="storage_bits_per_sample">8</property>
                                    <property name="storage_bits_per_sample_hidden">8</property>
                                    <property name="threads">1</property>
                                    <property name="threads_hidden">1</property>
                                    <property name="verticalBandwidthControl">1</property>
                                    <property name="verticalBandwidthControl_hidden">1</property>
                                    <componentName>Video Format Converter</componentName>
                                    <componentDefinitionName>Video Format Converter</componentDefinitionName>
                                    <componentDefinitionGuid>AC185E0C-6839-4dae-A547-5E18DF5EA058</componentDefinitionGuid>
                                    <componentOwningPluginName>VideoFormatConverter</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.VideoFormatConverter</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_IO"/>
                                    <pin name="out" type="OUTPUT_IO"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">493.9339599609375,145.71157836914062</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="cadenceReEntryMode">0</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="detect2224Cadence">true</property>
                                    <property name="detect22Cadence">true</property>
                                    <property name="detect2332Cadence">true</property>
                                    <property name="detect32322Cadence">true</property>
                                    <property name="detect32Cadence">true</property>
                                    <property name="detect55Cadence">true</property>
                                    <property name="detect64Cadence">true</property>
                                    <property name="detect87Cadence">true</property>
                                    <property name="latency">40</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <property name="m2LowNoiseModeFactor">1</property>
                                    <property name="noiseTolerance">0</property>
                                    <property name="outputFrameRate">matchInputFrameRate</property>
                                    <property name="remainIn2224Cadence">false</property>
                                    <property name="remainIn22Cadence">false</property>
                                    <property name="remainIn2332Cadence">false</property>
                                    <property name="remainIn32322Cadence">false</property>
                                    <property name="remainIn32Cadence">false</property>
                                    <property name="remainIn55Cadence">false</property>
                                    <property name="remainIn64Cadence">false</property>
                                    <property name="remainIn87Cadence">false</property>
                                    <property name="threads">1</property>
                                    <componentName>Deinterlacer</componentName>
                                    <componentDefinitionName>DRDeinterlacer</componentDefinitionName>
                                    <componentDefinitionGuid>750D51F3-FC19-410f-89AB-B7F3E8CAFEDC</componentDefinitionGuid>
                                    <componentOwningPluginName>DRDeinterlacer</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.DRDeinterlacer</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="out" type="OUTPUT_PUSH"/>
                                </component>
                            </childComponents>
                            <pin name="out" type="OUTPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">984.0,362.0</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="out" displayName="Out" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="in" type="INPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">984.0,362.0</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="in" displayName="In" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="in" type="INPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">560.0,173.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="in" displayName="Video" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Video Out" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">1874.2286376953125,73.51223754882812</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="Video Out" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">540.0,492.0</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="defaultInputPin">in 1</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>EIA-608 Captions Selector</componentName>
                    <componentDefinitionName>EIA-608 Captions Selector</componentDefinitionName>
                    <componentDefinitionGuid>0C2E5B8D-872A-4487-9D4D-07CECFD0F58C</componentDefinitionGuid>
                    <componentOwningPluginName>ClosedCaptionsUtilities</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.ClosedCaptionsUtilities</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in 1" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                    <pin name="in 2" type="INPUT_PUSH">
                        <pinDefinition name="in 2" displayName="EIA-608 Captions2" type="INPUT_PUSH" dynamic="true" dataTypeDefinitionName="Data608ServiceSample"/>
                    </pin>
                    <pin name="in 3" type="INPUT_PUSH">
                        <pinDefinition name="in 3" displayName="EIA-608 Captions3" type="INPUT_PUSH" dynamic="true" dataTypeDefinitionName="Data608ServiceSample"/>
                    </pin>
                    <pin name="in 4" type="INPUT_PUSH">
                        <pinDefinition name="in 4" displayName="EIA-608 Captions4" type="INPUT_PUSH" dynamic="true" dataTypeDefinitionName="Data608ServiceSample"/>
                    </pin>
                    <pin name="in 5" type="INPUT_PUSH">
                        <pinDefinition name="in 5" displayName="EIA-608 Captions5" type="INPUT_PUSH" dynamic="true" dataTypeDefinitionName="Data608ServiceSample"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">837.0,180.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio Logic and Encoding</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">229.99993896484375,50.02570343017578</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="accuracy_bits_per_sample" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="average_bit_rate" isNull="true"/>
                            <property name="bits_per_sample" isNull="true"/>
                            <property name="channel1_speaker">L_LEFT</property>
                            <property name="channel2_speaker">R_RIGHT</property>
                            <property name="channel3_speaker">C_CENTER</property>
                            <property name="channel4_speaker">LFE_LOW_FREQUENCY</property>
                            <property name="channel5_speaker">Ls_LEFT_SURROUND</property>
                            <property name="channel6_speaker">Rs_RIGHT_SURROUND</property>
                            <property name="channel7_speaker"></property>
                            <property name="channel8_speaker"></property>
                            <property name="channel_position_preset">L_R_C_LFE_Ls_Rs</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="encoder_preset_filter">WAVE</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="language_code" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="maximum_bit_rate" isNull="true"/>
                            <property name="num_active_channels" isNull="true"/>
                            <property name="num_channels" isNull="true"/>
                            <property name="out_endian">little</property>
                            <property name="override_input">false</property>
                            <property name="rate_control" isNull="true"/>
                            <property name="sample_rate" isNull="true"/>
                            <property name="sample_signed" isNull="true"/>
                            <property name="storage_bits_per_sample" isNull="true"/>
                            <componentName>Audio Data Type Updater</componentName>
                            <componentDefinitionName>Audio Data Type Updater</componentDefinitionName>
                            <componentDefinitionGuid>9D095BEC-5A2C-445e-9AF9-A17313693263</componentDefinitionGuid>
                            <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">248.9998779296875,154.02570343017578</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="num_channels"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="5"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="dynamicSwitching" isNull="true"/>
                            <property name="evaluateEndOfStream">false</property>
                            <property name="logicOperator">and</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Branch 2</componentName>
                            <componentDefinitionName>Logic Branch</componentDefinitionName>
                            <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="yes" type="OUTPUT_PUSH"/>
                            <pin name="no" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">322.9998779296875,287.0257034301758</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="num_channels"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="4"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="dynamicSwitching" isNull="true"/>
                            <property name="evaluateEndOfStream">false</property>
                            <property name="logicOperator">and</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Branch 3</componentName>
                            <componentDefinitionName>Logic Branch</componentDefinitionName>
                            <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="yes" type="OUTPUT_PUSH"/>
                            <pin name="no" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">575.3209228515625,154.52320098876953</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="accuracy_bits_per_sample" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="average_bit_rate" isNull="true"/>
                            <property name="bits_per_sample" isNull="true"/>
                            <property name="channel1_speaker">L_LEFT</property>
                            <property name="channel2_speaker">R_RIGHT</property>
                            <property name="channel3_speaker">Ls_LEFT_SURROUND</property>
                            <property name="channel4_speaker">Rs_RIGHT_SURROUND</property>
                            <property name="channel5_speaker">LFE_LOW_FREQUENCY</property>
                            <property name="channel6_speaker"></property>
                            <property name="channel7_speaker"></property>
                            <property name="channel8_speaker"></property>
                            <property name="channel_position_preset">L_R_Ls_Rs</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="encoder_preset_filter"></property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="language_code" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="maximum_bit_rate" isNull="true"/>
                            <property name="num_active_channels" isNull="true"/>
                            <property name="num_channels" isNull="true"/>
                            <property name="out_endian">little</property>
                            <property name="override_input">false</property>
                            <property name="rate_control" isNull="true"/>
                            <property name="sample_rate" isNull="true"/>
                            <property name="sample_signed" isNull="true"/>
                            <property name="storage_bits_per_sample" isNull="true"/>
                            <componentName>Audio Data Type Updater 2</componentName>
                            <componentDefinitionName>Audio Data Type Updater</componentDefinitionName>
                            <componentDefinitionGuid>9D095BEC-5A2C-445e-9AF9-A17313693263</componentDefinitionGuid>
                            <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">554.9998779296875,50.02570343017578</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="center_gain_db">-3.0</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="surround_gain_db">-3.0</property>
                            <componentName>Down Mix 5.1 to Stereo</componentName>
                            <componentDefinitionName>Down Mix 5.1 to Stereo</componentDefinitionName>
                            <componentDefinitionGuid>9B591F12-F688-4776-8F56-EC22E1DC367E</componentDefinitionGuid>
                            <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">618.3209228515625,275.52320098876953</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="accuracy_bits_per_sample" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="average_bit_rate" isNull="true"/>
                            <property name="bits_per_sample" isNull="true"/>
                            <property name="channel1_speaker">L_LEFT</property>
                            <property name="channel2_speaker">R_RIGHT</property>
                            <property name="channel3_speaker">Ls_LEFT_SURROUND</property>
                            <property name="channel4_speaker">Rs_RIGHT_SURROUND</property>
                            <property name="channel5_speaker"></property>
                            <property name="channel6_speaker"></property>
                            <property name="channel7_speaker"></property>
                            <property name="channel8_speaker"></property>
                            <property name="channel_position_preset">L_R_Ls_Rs</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="encoder_preset_filter">WAVE</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="language_code" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="maximum_bit_rate" isNull="true"/>
                            <property name="num_active_channels" isNull="true"/>
                            <property name="num_channels" isNull="true"/>
                            <property name="out_endian">little</property>
                            <property name="override_input">false</property>
                            <property name="rate_control" isNull="true"/>
                            <property name="sample_rate" isNull="true"/>
                            <property name="sample_signed" isNull="true"/>
                            <property name="storage_bits_per_sample" isNull="true"/>
                            <componentName>Audio Data Type Updater 3</componentName>
                            <componentDefinitionName>Audio Data Type Updater</componentDefinitionName>
                            <componentDefinitionGuid>9D095BEC-5A2C-445e-9AF9-A17313693263</componentDefinitionGuid>
                            <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">680.9998779296875,406.0257034301758</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="accuracy_bits_per_sample" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="average_bit_rate" isNull="true"/>
                            <property name="bits_per_sample" isNull="true"/>
                            <property name="channel1_speaker">L_LEFT</property>
                            <property name="channel2_speaker">R_RIGHT</property>
                            <property name="channel3_speaker">C_CENTER</property>
                            <property name="channel4_speaker"></property>
                            <property name="channel5_speaker"></property>
                            <property name="channel6_speaker"></property>
                            <property name="channel7_speaker"></property>
                            <property name="channel8_speaker"></property>
                            <property name="channel_position_preset">L_R_C</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="encoder_preset_filter">WAVE</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="language_code" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="maximum_bit_rate" isNull="true"/>
                            <property name="num_active_channels" isNull="true"/>
                            <property name="num_channels" isNull="true"/>
                            <property name="out_endian">little</property>
                            <property name="override_input">false</property>
                            <property name="rate_control" isNull="true"/>
                            <property name="sample_rate" isNull="true"/>
                            <property name="sample_signed" isNull="true"/>
                            <property name="storage_bits_per_sample" isNull="true"/>
                            <componentName>Audio Data Type Updater 4</componentName>
                            <componentDefinitionName>Audio Data Type Updater</componentDefinitionName>
                            <componentDefinitionGuid>9D095BEC-5A2C-445e-9AF9-A17313693263</componentDefinitionGuid>
                            <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">444.9998779296875,424.0257034301758</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="num_channels"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="3"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="dynamicSwitching" isNull="true"/>
                            <property name="evaluateEndOfStream">false</property>
                            <property name="logicOperator">and</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Branch 4</componentName>
                            <componentDefinitionName>Logic Branch</componentDefinitionName>
                            <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="yes" type="OUTPUT_PUSH"/>
                            <pin name="no" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">633.9998779296875,543.0257034301758</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="num_channels"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="2"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="dynamicSwitching" isNull="true"/>
                            <property name="evaluateEndOfStream">false</property>
                            <property name="logicOperator">and</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Branch 5</componentName>
                            <componentDefinitionName>Logic Branch</componentDefinitionName>
                            <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="yes" type="OUTPUT_PUSH"/>
                            <pin name="no" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">847.9998779296875,536.0257034301758</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="accuracy_bits_per_sample" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="average_bit_rate" isNull="true"/>
                            <property name="bits_per_sample" isNull="true"/>
                            <property name="channel1_speaker">L_LEFT</property>
                            <property name="channel2_speaker">R_RIGHT</property>
                            <property name="channel3_speaker"></property>
                            <property name="channel4_speaker"></property>
                            <property name="channel5_speaker"></property>
                            <property name="channel6_speaker"></property>
                            <property name="channel7_speaker"></property>
                            <property name="channel8_speaker"></property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="encoder_preset_filter">WAVE</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="language_code" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="maximum_bit_rate" isNull="true"/>
                            <property name="num_active_channels" isNull="true"/>
                            <property name="num_channels" isNull="true"/>
                            <property name="out_endian">little</property>
                            <property name="override_input">false</property>
                            <property name="rate_control" isNull="true"/>
                            <property name="sample_rate" isNull="true"/>
                            <property name="sample_signed" isNull="true"/>
                            <property name="storage_bits_per_sample" isNull="true"/>
                            <componentName>Audio Data Type Updater 5</componentName>
                            <componentDefinitionName>Audio Data Type Updater</componentDefinitionName>
                            <componentDefinitionGuid>9D095BEC-5A2C-445e-9AF9-A17313693263</componentDefinitionGuid>
                            <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">1171.553955078125,233.67517852783203</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in1</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Branch Merger</componentName>
                            <componentDefinitionName>BranchMerger</componentDefinitionName>
                            <componentDefinitionGuid>5442b6d8-5672-430f-91ce-5b047e84bd7f</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in1" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                            <pin name="in2" type="INPUT_PUSH">
                                <pinDefinition name="in2" displayName="In 2" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in3" type="INPUT_PUSH">
                                <pinDefinition name="in3" displayName="In 3" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in4" type="INPUT_PUSH">
                                <pinDefinition name="in4" displayName="In 4" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in5" type="INPUT_PUSH">
                                <pinDefinition name="in5" displayName="In 5" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in6" type="INPUT_PUSH">
                                <pinDefinition name="in6" displayName="In 6" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in7" type="INPUT_PUSH">
                                <pinDefinition name="in7" displayName="In 7" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">5.9998779296875,50.02570343017578</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;EQUALS&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="num_channels"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="6"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="dynamicSwitching" isNull="true"/>
                            <property name="evaluateEndOfStream">false</property>
                            <property name="logicOperator">and</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Branch</componentName>
                            <componentDefinitionName>Logic Branch</componentDefinitionName>
                            <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="yes" type="OUTPUT_PUSH"/>
                            <pin name="no" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1902.5883178710938,97.1932373046875</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate">44100</property>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">false</property>
                            <componentName>AAC Encoder - 192kbps</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1444.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Audio Routing - based on Source Video Framesize</componentName>
                            <componentDefinitionName>Kayak Graph</componentDefinitionName>
                            <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                            <componentOwningPluginName>KayakCore</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                            <childComponents>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">14.9884033203125,14.988357543945312</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;GREATER_THAN&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="1920"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin" isNull="true"/>
                                    <property name="dynamicSwitching" isNull="true"/>
                                    <property name="evaluateEndOfStream">false</property>
                                    <property name="logicOperator">and</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Branch</componentName>
                                    <componentDefinitionName>Logic Branch</componentDefinitionName>
                                    <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="yes" type="OUTPUT_PUSH"/>
                                    <pin name="no" type="OUTPUT_PUSH"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">944.26513671875,0.0</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Gate</componentName>
                                    <componentDefinitionName>Logic Gate</componentDefinitionName>
                                    <componentDefinitionGuid>1824FAE2-9B42-4bb1-89D8-41104BC1B76C</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="ingate" type="INPUT_PUSH"/>
                                    <pin name="out" type="OUTPUT_PUSH"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">943.5633544921875,113.7545394897461</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Gate 2</componentName>
                                    <componentDefinitionName>Logic Gate</componentDefinitionName>
                                    <componentDefinitionGuid>1824FAE2-9B42-4bb1-89D8-41104BC1B76C</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="ingate" type="INPUT_PUSH"/>
                                    <pin name="out" type="OUTPUT_PUSH"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">236.7255859375,148.52085876464844</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;LESS_THAN_OR_EQUAL&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="1920"/&gt;
        &lt;/Condition&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;GREATER_THAN&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="1280"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin" isNull="true"/>
                                    <property name="dynamicSwitching" isNull="true"/>
                                    <property name="evaluateEndOfStream">false</property>
                                    <property name="logicOperator">and</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Branch 2</componentName>
                                    <componentDefinitionName>Logic Branch</componentDefinitionName>
                                    <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="yes" type="OUTPUT_PUSH"/>
                                    <pin name="no" type="OUTPUT_PUSH"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">495.2757568359375,250.01222229003906</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;LESS_THAN_OR_EQUAL&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="1280"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin" isNull="true"/>
                                    <property name="dynamicSwitching">true</property>
                                    <property name="evaluateEndOfStream">false</property>
                                    <property name="logicOperator">and</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Branch 2 2</componentName>
                                    <componentDefinitionName>Logic Branch</componentDefinitionName>
                                    <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="yes" type="OUTPUT_PUSH"/>
                                    <pin name="no" type="OUTPUT_PUSH"/>
                                </component>
                                <component>
                                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                                    <property name="_graphDisplayContents" isNull="true"/>
                                    <property name="_graphDisplayLocation">938.7737426757812,227.5090560913086</property>
                                    <property name="_timeBase_local" isNull="true"/>
                                    <property name="defaultInputPin">in</property>
                                    <property name="defaultOutputPin">out</property>
                                    <property name="logsMaxEntries" isNull="true"/>
                                    <componentName>Logic Gate 2 2</componentName>
                                    <componentDefinitionName>Logic Gate</componentDefinitionName>
                                    <componentDefinitionGuid>1824FAE2-9B42-4bb1-89D8-41104BC1B76C</componentDefinitionGuid>
                                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                                    <childComponents/>
                                    <pin name="in" type="INPUT_PUSH"/>
                                    <pin name="ingate" type="INPUT_PUSH"/>
                                    <pin name="out" type="OUTPUT_PUSH"/>
                                </component>
                            </childComponents>
                            <pin name="Video Input" type="INPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">22.58319091796875,62.65520477294922</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="Video Input" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Audio Input" type="INPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">-45.2413330078125,42.216575622558594</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="Audio Input" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Audio Out - 4K" type="OUTPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">750.8654174804688,59.93006134033203</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="Audio Out - 4K" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Audio Out - 1080" type="OUTPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">1159.63818359375,156.67294311523438</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="Audio Out - 1080" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Audio Out - 1280 and less" type="OUTPUT_IO">
                                <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                                <property name="_graphDisplayLocation">1197.790283203125,119.88339233398438</property>
                                <property name="_userDeleteable">true</property>
                                <pinDefinition name="Audio Out - 1280 and less" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1905.5883178710938,194.1115264892578</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate">44100</property>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">false</property>
                            <componentName>AAC Encoder - 128kbps</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">2356.7916870117188,102.98943328857422</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in1</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Branch Merger 2</componentName>
                            <componentDefinitionName>BranchMerger</componentDefinitionName>
                            <componentDefinitionGuid>5442b6d8-5672-430f-91ce-5b047e84bd7f</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in1" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                            <pin name="in2" type="INPUT_PUSH">
                                <pinDefinition name="in2" displayName="In 2" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in3" type="INPUT_PUSH">
                                <pinDefinition name="in3" displayName="In 3" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in4" type="INPUT_PUSH">
                                <pinDefinition name="in4" displayName="In 4" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1902.1943359375,0.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">256000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate">44100</property>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">false</property>
                            <componentName>AAC Encoder - 256kbps</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="Duration">Depend on the shortest source</property>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">858.0,690.0</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">out</property>
                            <property name="framesPerPacket">1024</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Audio Stream Interleaver</componentName>
                            <componentDefinitionName>Audio Stream Interleaver</componentDefinitionName>
                            <componentDefinitionGuid>D166D48B-FA26-44ca-8F2D-62B20D892659</componentDefinitionGuid>
                            <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                            <childComponents/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                            <pin name="done" type="EVENT"/>
                            <pin name="in 1" type="INPUT_PUSH">
                                <pinDefinition name="in 1" displayName="Raw Audio 1" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in 2" type="INPUT_PUSH">
                                <pinDefinition name="in 2" displayName="Raw Audio 2" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in 3" type="INPUT_PUSH">
                                <pinDefinition name="in 3" displayName="Raw Audio 3" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1100.9998779296875,677.0257034301758</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="accuracy_bits_per_sample" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="average_bit_rate" isNull="true"/>
                            <property name="bits_per_sample" isNull="true"/>
                            <property name="channel1_speaker">L_LEFT</property>
                            <property name="channel2_speaker">R_RIGHT</property>
                            <property name="channel3_speaker"></property>
                            <property name="channel4_speaker"></property>
                            <property name="channel5_speaker"></property>
                            <property name="channel6_speaker"></property>
                            <property name="channel7_speaker"></property>
                            <property name="channel8_speaker"></property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="encoder_preset_filter">WAVE</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="language_code" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="maximum_bit_rate" isNull="true"/>
                            <property name="num_active_channels" isNull="true"/>
                            <property name="num_channels" isNull="true"/>
                            <property name="out_endian">little</property>
                            <property name="override_input">false</property>
                            <property name="rate_control" isNull="true"/>
                            <property name="sample_rate" isNull="true"/>
                            <property name="sample_signed" isNull="true"/>
                            <property name="storage_bits_per_sample" isNull="true"/>
                            <componentName>Audio Data Type Updater 6</componentName>
                            <componentDefinitionName>Audio Data Type Updater</componentDefinitionName>
                            <componentDefinitionGuid>9D095BEC-5A2C-445e-9AF9-A17313693263</componentDefinitionGuid>
                            <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                    </childComponents>
                    <pin name="Audio Input" type="INPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">-69.06365966796875,39.58576202392578</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="Audio Input" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="out" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">1728.9998779296875,79.99994659423828</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="out" displayName="Compressed Audio (AAC)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Video Input" type="INPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">1404.501220703125,170.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="Video Input" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">843.0,479.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="allow_caption_splitting">FALSE</property>
                    <property name="always_set_timeend_on_last_packet">TRUE</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="null_packet_threshold">300</property>
                    <property name="rendering_mode">preserved</property>
                    <componentName>EIA-608 Captions Decoder</componentName>
                    <componentDefinitionName>Advanced 608 Closed Captions Decoder</componentDefinitionName>
                    <componentDefinitionGuid>435A116E-392C-4300-B9A9-26FD56C95063</componentDefinitionGuid>
                    <componentOwningPluginName>CC608Decoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CC608Decoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="timing" type="INPUT_IO"/>
                    <pin name="out_cc1" type="OUTPUT_IO"/>
                    <pin name="out_cc2" type="OUTPUT_IO"/>
                    <pin name="out_cc3" type="OUTPUT_IO"/>
                    <pin name="out_cc4" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">1164.0,449.0</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="defaultInputPin">in 1</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Timed Text Selector</componentName>
                    <componentDefinitionName>Timed Text Selector</componentDefinitionName>
                    <componentDefinitionGuid>8F552A81-B73E-46fA-9E14-145EC7DF3001</componentDefinitionGuid>
                    <componentOwningPluginName>TimedTextSelector</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.TimedTextSelector</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in 1" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                    <pin name="in 2" type="INPUT_PUSH">
                        <pinDefinition name="in 2" displayName="Timed Text 2" type="INPUT_PUSH" dynamic="true" dataTypeDefinitionName="TimedTextSample"/>
                    </pin>
                    <pin name="in 3" type="INPUT_PUSH">
                        <pinDefinition name="in 3" displayName="Timed Text 3" type="INPUT_PUSH" dynamic="true" dataTypeDefinitionName="TimedTextSample"/>
                    </pin>
                    <pin name="in 4" type="INPUT_PUSH">
                        <pinDefinition name="in 4" displayName="Timed Text 4" type="INPUT_PUSH" dynamic="true" dataTypeDefinitionName="TimedTextSample"/>
                    </pin>
                    <pin name="in 5" type="INPUT_PUSH">
                        <pinDefinition name="in 5" displayName="Timed Text 5" type="INPUT_PUSH" dynamic="true" dataTypeDefinitionName="TimedTextSample"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">1374.0,449.0</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="language_code">en</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="override_language_code">FALSE</property>
                    <componentName>Language Code Updater</componentName>
                    <componentDefinitionName>Language Code Updater</componentDefinitionName>
                    <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                    <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">1633.0,449.0</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="color_representation">verbose</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="escape_special_characters">FALSE</property>
                    <property name="exact_duration">FALSE</property>
                    <property name="font_family">monospace</property>
                    <property name="font_size">15%</property>
                    <property name="grid_height">15</property>
                    <property name="grid_width">32</property>
                    <property name="group_paragraphs">FALSE</property>
                    <property name="image_height" isNull="true"/>
                    <property name="image_size_accounting" isNull="true"/>
                    <property name="image_width" isNull="true"/>
                    <property name="include_smpte_information_header">true</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">TRUE</property>
                    <property name="max_segment_size">120</property>
                    <property name="max_xml_size">20</property>
                    <property name="pixel_count">2097152</property>
                    <property name="popup">pop</property>
                    <property name="preset">smptett</property>
                    <property name="region_dimensions_format" isNull="true"/>
                    <property name="region_dimensions_location">paragraph</property>
                    <property name="scaling">90</property>
                    <property name="segment_duration">30</property>
                    <property name="segment_limits">duration</property>
                    <property name="segment_time_base">absolute</property>
                    <property name="split_multiline">TRUE</property>
                    <property name="style">basic</property>
                    <property name="subt040043_workaround">false</property>
                    <property name="text_align">left</property>
                    <property name="time_format">clock_time</property>
                    <property name="time_precision">2</property>
                    <componentName>TTML Encoder</componentName>
                    <componentDefinitionName>TTML Encoder</componentDefinitionName>
                    <componentDefinitionGuid>1BED774D-D15A-444a-BD59-E113477BD6A6</componentDefinitionGuid>
                    <componentOwningPluginName>TTMLEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.TTMLEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_PUSH"/>
                    <pin name="video" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="Decode_captions_AFD">true</property>
                    <property name="EndTime" isNull="true"/>
                    <property name="StartTime" isNull="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">152.0,125.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="always_use_directshow">false</property>
                    <property name="blackThreshold">0.10</property>
                    <property name="black_border_detection">false</property>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="enable_directshow">false</property>
                    <property name="filename" marshallerKey="Serializable">rO0ABXNyADVjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGEuaW1wbC5EZWZhdWx0RGF0YUNvbnRh
aW5lcgAAAAAAAAABAgAEWgAHbXV0YWJsZUoAC3NpemVJbkJ5dGVzTAAKZGF0YU9iamVjdHQAEkxq
YXZhL2xhbmcvT2JqZWN0O0wACGRhdGFUeXBldAAmTGNhL2RpZ2l0YWxyYXBpZHMva2F5YWsvZGF0
YS9EYXRhVHlwZTt4cAEAAAAAAAAAAXQATUY6XEltYWdpbmUgQ29tbXVuaWNhdGlvbnNcU291cmNl
IEZpbGVzXE1YRl9YRENBTUhEXzEwODBpMjlfOHhNb25vX0FuY0RhdGEubXhmc3IALWNhLmRpZ2l0
YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLkJhc2VEYXRhVHlwZQAAAAAAAAABAgADWgAHbXV0YWJs
ZUwAEmRhdGFUeXBlRGVmaW5pdGlvbnQARkxjYS9kaWdpdGFscmFwaWRzL2theWFrL2RhdGF0eXBl
cy9kZWZpbml0aW9uL21vZGVsL0RhdGFUeXBlRGVmaW5pdGlvbjtMAANtYXB0AA9MamF2YS91dGls
L01hcDt4cAFwc3IAEWphdmEudXRpbC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJ
dGhyZXNob2xkeHA/AAAAAAAAQHcIAAAAQAAAAAB4</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="inspection_max_megabytes" isNull="true"/>
                    <property name="inspection_max_seconds" isNull="true"/>
                    <property name="inspection_mode" isNull="true"/>
                    <property name="logFile" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="noiseThreshold">0.10</property>
                    <property name="probeDuration">60.0</property>
                    <property name="probeRate">0.10</property>
                    <property name="probeTimeInterval">1.0</property>
                    <componentName>Media File Input</componentName>
                    <componentDefinitionName>Media File Input</componentDefinitionName>
                    <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                    <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                    <childComponents/>
                    <pin name="filename" type="PROPERTY">
                        <property name="_pinProperty">filename</property>
                    </pin>
                    <pin name="Timecode" type="OUTPUT_IO">
                        <pinDefinition name="Timecode" displayName="Timecode (GOP Header)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedVideo" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedVideo" displayName="Uncompressed Video" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio 2" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio 2" displayName="Uncompressed Audio 2" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio 3" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio 3" displayName="Uncompressed Audio 3" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio 4" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio 4" displayName="Uncompressed Audio 4" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio 5" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio 5" displayName="Uncompressed Audio 5" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio 6" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio 6" displayName="Uncompressed Audio 6" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data608Service" type="OUTPUT_IO">
                        <pinDefinition name="Data608Service" displayName="EIA-608 Captions (SCTE-20 User Data)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data608Service 2" type="OUTPUT_IO">
                        <pinDefinition name="Data608Service 2" displayName="EIA-608 Captions (708 compatibility bytes)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data708Service" type="OUTPUT_IO">
                        <pinDefinition name="Data708Service" displayName="EIA-708 Captions (ATSC User Data)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 2" type="OUTPUT_IO">
                        <pinDefinition name="Timecode 2" displayName="Timecode (328M)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="MPEGUserData" type="OUTPUT_IO">
                        <pinDefinition name="MPEGUserData" displayName="User Data" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedVideo" type="OUTPUT_IO">
                        <pinDefinition name="CompressedVideo" displayName="Compressed Video (MPEG-1 or MPEG-2)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio" type="OUTPUT_IO">
                        <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (AES)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio 2" type="OUTPUT_IO">
                        <pinDefinition name="CompressedAudio 2" displayName="Uncompressed Audio 2 (AES)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio 3" type="OUTPUT_IO">
                        <pinDefinition name="CompressedAudio 3" displayName="Uncompressed Audio 3 (AES)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio 4" type="OUTPUT_IO">
                        <pinDefinition name="CompressedAudio 4" displayName="Uncompressed Audio 4 (AES)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio 5" type="OUTPUT_IO">
                        <pinDefinition name="CompressedAudio 5" displayName="Uncompressed Audio 5 (AES)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio 6" type="OUTPUT_IO">
                        <pinDefinition name="CompressedAudio 6" displayName="Uncompressed Audio 6 (AES)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio 7" type="OUTPUT_IO">
                        <pinDefinition name="CompressedAudio 7" displayName="Uncompressed Audio 7 (AES)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio 8" type="OUTPUT_IO">
                        <pinDefinition name="CompressedAudio 8" displayName="Uncompressed Audio 8 (AES)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 3" type="OUTPUT_IO">
                        <pinDefinition name="Timecode 3" displayName="Timecode (LTC)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 4" type="OUTPUT_IO">
                        <pinDefinition name="Timecode 4" displayName="Timecode (VITC)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio 7" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio 7" displayName="Uncompressed Audio 7" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio 8" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio 8" displayName="Uncompressed Audio 8" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="ActiveFormatDescription" type="OUTPUT_IO">
                        <pinDefinition name="ActiveFormatDescription" displayName="Active Format Description" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data608Service 3" type="OUTPUT_IO">
                        <pinDefinition name="Data608Service 3" displayName="EIA-608 Captions (Ancillary Data)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data608Service 4" type="OUTPUT_IO">
                        <pinDefinition name="Data608Service 4" displayName="EIA-608 Captions 2 (708 compatibility bytes)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data708Service 2" type="OUTPUT_IO">
                        <pinDefinition name="Data708Service 2" displayName="EIA-708 Captions (Ancillary Data)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="AncillaryData" type="OUTPUT_IO">
                        <pinDefinition name="AncillaryData" displayName="Ancillary Data" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 5" type="OUTPUT_IO">
                        <pinDefinition name="Timecode 5" displayName="Timecode (MXF Material Package)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 6" type="OUTPUT_IO">
                        <pinDefinition name="Timecode 6" displayName="Timecode (MXF System Track)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1292.4613342285156,28.081722259521484</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Video</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">270.6908874511719,29.170696258544922</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;GREATER_THAN&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="1920"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="dynamicSwitching" isNull="true"/>
                            <property name="evaluateEndOfStream">false</property>
                            <property name="logicOperator">and</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Branch</componentName>
                            <componentDefinitionName>Logic Branch</componentDefinitionName>
                            <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="yes" type="OUTPUT_PUSH"/>
                            <pin name="no" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">501.1117858886719,213.8457145690918</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;LESS_THAN_OR_EQUAL&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="1920"/&gt;
        &lt;/Condition&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;GREATER_THAN&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="1280"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="dynamicSwitching" isNull="true"/>
                            <property name="evaluateEndOfStream">false</property>
                            <property name="logicOperator">and</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Branch 2</componentName>
                            <componentDefinitionName>Logic Branch</componentDefinitionName>
                            <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="yes" type="OUTPUT_PUSH"/>
                            <pin name="no" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">751.2992858886719,361.3370780944824</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;LESS_THAN_OR_EQUAL&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="1280"/&gt;
        &lt;/Condition&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;GREATER_THAN&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="720"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="dynamicSwitching" isNull="true"/>
                            <property name="evaluateEndOfStream">false</property>
                            <property name="logicOperator">and</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Branch 2 2</componentName>
                            <componentDefinitionName>Logic Branch</componentDefinitionName>
                            <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="yes" type="OUTPUT_PUSH"/>
                            <pin name="no" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition displayName="Target Video Format" name="video_format" group="General" dynamic="true">
                                <initialValue>Same as Input(1tc/frame)</initialValue>
                                <valueType type="STRING">
                                    <valueRestriction strictEnum="true">
                                        <enumerationValue val="23.98i(1tc/frame)" displayName="23.98 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="23.98i(1tc/field)" displayName="23.98 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="24i(1tc/frame)" displayName="24 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="24i(1tc/field)" displayName="24 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="25i(1tc/frame)" displayName="25 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="25i(1tc/field)" displayName="25 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="29.97i(1tc/frame)" displayName="29.97 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="29.97i(1tc/field)" displayName="29.97 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="30i(1tc/frame)" displayName="30 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="30i(1tc/field)" displayName="30 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="50p" displayName="50 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="59.94p" displayName="59.94 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="60p" displayName="60 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="Same as Input(1tc/frame)" displayName="Input (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="Same as Input(1tc/field)" displayName="Input (inserted per field)"></enumerationValue>
                                    </valueRestriction>
                                </valueType>
                            </propertyDefinition>
                            <propertyDefinition displayName="Timecode Setting" name="start_timecode" group="General" dynamic="true">
                                <initialValue>00:00:00:00/30</initialValue>
                                <valueType type="TIMECODE"/>
                            </propertyDefinition>
                            <property name="Aspect Ratio" isNull="true"/>
                            <property name="Frame Rate" isNull="true"/>
                            <property name="General.extended_sar" isNull="true"/>
                            <property name="General.sar" isNull="true"/>
                            <property name="Timecode">none</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1387.3326721191406,0.0</property>
                            <property name="_graphMinDisplaySize">1052.0,214.0</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="_useSerializedDataTypeDefs" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="adv.custom_frame_rate_timescale">240000</property>
                            <property name="adv.direct_mode">0</property>
                            <property name="adv.enable_custom_frame_rate_timescale">false</property>
                            <property name="adv.intra_precision">2</property>
                            <property name="adv.mbaff_scan_type">1</property>
                            <property name="adv.ps3_mode">0</property>
                            <property name="adv.qt_mode">0</property>
                            <property name="adv.transform_8x8">0</property>
                            <property name="adv.weighted_pred_flag">1</property>
                            <property name="alternateJava" isNull="true"/>
                            <property name="cadenceReEntryMode">0</property>
                            <property name="defaultInputPin">Video</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="detect2224Cadence">true</property>
                            <property name="detect22Cadence">true</property>
                            <property name="detect2332Cadence">true</property>
                            <property name="detect32322Cadence">true</property>
                            <property name="detect32Cadence">true</property>
                            <property name="detect55Cadence">true</property>
                            <property name="detect64Cadence">true</property>
                            <property name="detect87Cadence">true</property>
                            <property name="dominance">pass_through</property>
                            <property name="edge_smoothing" isNull="true"/>
                            <property name="filterControl">0</property>
                            <property name="filterType">0</property>
                            <property name="gen.enable_3d_encoding">false</property>
                            <property name="gen.level_idc">51</property>
                            <property name="gen.mbaff_mode">false</property>
                            <property name="gen.profile_idc">100</property>
                            <property name="gen.speed">0</property>
                            <property name="gen.sym_mode">1</property>
                            <property name="gop.adaptive_bframes">true</property>
                            <property name="gop.bframes">2</property>
                            <property name="gop.duration">2</property>
                            <property name="gop.idr_control">1</property>
                            <property name="gop.idr_period">1</property>
                            <property name="gop.min_intra_period">4</property>
                            <property name="gop.mode">0</property>
                            <property name="gop.sps_period">0</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="interpolation">CUBIC2P_BSPLINE</property>
                            <property name="javaSelector" isNull="true"/>
                            <property name="latency">30</property>
                            <property name="legacyDataTypeSerialization" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="me.max_refs">1</property>
                            <property name="me.search_range">-1</property>
                            <property name="me.subdiv">7</property>
                            <property name="mt.max_pict_tasks">-1</property>
                            <property name="mt.mode">2</property>
                            <property name="mt.num_threads">8</property>
                            <property name="noiseTolerance">0</property>
                            <property name="oopLaunchTimeoutSeconds" isNull="true"/>
                            <property name="outputFrameRate">matchInputFieldRate</property>
                            <property name="output_height" isNull="true"/>
                            <property name="output_width" isNull="true"/>
                            <property name="pluginOptions" isNull="true"/>
                            <property name="preset">yuyv</property>
                            <property name="pulldown_mode">2:3TFF</property>
                            <property name="rc.auto_qp">true</property>
                            <property name="rc.initial_cpb_removal_delay">-1</property>
                            <property name="rc.kbps">12000</property>
                            <property name="rc.max_intra_frame_bytes">0</property>
                            <property name="rc.max_kbps">0</property>
                            <property name="rc.min_intra_frame_bytes">0</property>
                            <property name="rc.qp_delta_b">4</property>
                            <property name="rc.qp_delta_p">2</property>
                            <property name="rc.qp_intra">30</property>
                            <property name="rc.qp_max">51</property>
                            <property name="rc.qp_min">8</property>
                            <property name="rc.scene_detect">35</property>
                            <property name="rc.type">2</property>
                            <property name="rc.unrestricted_iframepicsize">0</property>
                            <property name="rc.vbv_length">1000</property>
                            <property name="runOutOfProcess">if32bit</property>
                            <property name="sei.pic_timing_flag">1</property>
                            <property name="sei.split_sei_payload">0</property>
                            <property name="shift">shift_up</property>
                            <property name="showOutOfProcessGUI" isNull="true"/>
                            <property name="slice.deblock">1</property>
                            <property name="slice.mode">0</property>
                            <property name="slice.param">1</property>
                            <property name="start_timecode">00:00:00:00/30</property>
                            <property name="threads">1</property>
                            <property name="verticalBandwidthControl">1</property>
                            <property name="video_format">Same as Input(1tc/field)</property>
                            <property name="vui.bitstream_restriction_flag">false</property>
                            <property name="vui.colour_description_present_flag">false</property>
                            <property name="vui.colour_primaries">2</property>
                            <property name="vui.matrix_coeffients">2</property>
                            <property name="vui.max_bytes_per_pic_denom">false</property>
                            <property name="vui.nal_hrd_parameters_present_flag">false</property>
                            <property name="vui.pic_struct_present_flag">true</property>
                            <property name="vui.timing_info_present_flag">false</property>
                            <property name="vui.transfer_characteristics">2</property>
                            <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                            <property name="vui.video_format">2</property>
                            <property name="vui.video_signal_type_present_flag">true</property>
                            <componentName>AVC Video Encoder</componentName>
                            <componentDefinitionName>Advanced AVC Encoder</componentDefinitionName>
                            <componentDefinitionGuid>A3597472-D51E-44d9-9F0A-395744A83FA3</componentDefinitionGuid>
                            <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="Video" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                            <pin name="Timecode (SEI)" type="INPUT_IO"/>
                            <pin name="EIA-608 Captions" type="INPUT_IO"/>
                            <pin name="EIA-708 Captions" type="INPUT_IO">
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                                <property name="_graphDisplayLocation">-61,102</property>
                            </pin>
                            <pin name="User Data" type="INPUT_IO">
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                                <property name="_graphDisplayLocation">-57,133</property>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">932.7206726074219,533.7467765808105</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="conditionsXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;LogicRules&gt;
    &lt;Conditions&gt;
        &lt;Condition&gt;
            &lt;compareOperation&gt;LESS_THAN_OR_EQUAL&lt;/compareOperation&gt;
            &lt;lhs source="DATA_TYPE" value="image_width"/&gt;
            &lt;rhs source="EXPLICIT_VALUE" value="720"/&gt;
        &lt;/Condition&gt;
    &lt;/Conditions&gt;
&lt;/LogicRules&gt;
</property>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="dynamicSwitching" isNull="true"/>
                            <property name="evaluateEndOfStream">false</property>
                            <property name="logicOperator">and</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Branch 2 2 2</componentName>
                            <componentDefinitionName>Logic Branch</componentDefinitionName>
                            <componentDefinitionGuid>d3539080-4b93-48d5-a616-66de67cdd66a</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="yes" type="OUTPUT_PUSH"/>
                            <pin name="no" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition displayName="Target Video Format" name="video_format" group="General" dynamic="true">
                                <initialValue>Same as Input(1tc/frame)</initialValue>
                                <valueType type="STRING">
                                    <valueRestriction strictEnum="true">
                                        <enumerationValue val="23.98i(1tc/frame)" displayName="23.98 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="23.98i(1tc/field)" displayName="23.98 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="24i(1tc/frame)" displayName="24 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="24i(1tc/field)" displayName="24 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="25i(1tc/frame)" displayName="25 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="25i(1tc/field)" displayName="25 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="29.97i(1tc/frame)" displayName="29.97 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="29.97i(1tc/field)" displayName="29.97 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="30i(1tc/frame)" displayName="30 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="30i(1tc/field)" displayName="30 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="50p" displayName="50 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="59.94p" displayName="59.94 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="60p" displayName="60 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="Same as Input(1tc/frame)" displayName="Input (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="Same as Input(1tc/field)" displayName="Input (inserted per field)"></enumerationValue>
                                    </valueRestriction>
                                </valueType>
                            </propertyDefinition>
                            <propertyDefinition displayName="Timecode Setting" name="start_timecode" group="General" dynamic="true">
                                <initialValue>00:00:00:00/30</initialValue>
                                <valueType type="TIMECODE"/>
                            </propertyDefinition>
                            <property name="Aspect Ratio" isNull="true"/>
                            <property name="Frame Rate" isNull="true"/>
                            <property name="General.extended_sar" isNull="true"/>
                            <property name="General.sar" isNull="true"/>
                            <property name="Timecode">none</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1389.1727600097656,344.73536682128906</property>
                            <property name="_graphMinDisplaySize">1052.0,214.0</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="_useSerializedDataTypeDefs" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="adv.custom_frame_rate_timescale">240000</property>
                            <property name="adv.direct_mode">0</property>
                            <property name="adv.enable_custom_frame_rate_timescale">false</property>
                            <property name="adv.intra_precision">2</property>
                            <property name="adv.mbaff_scan_type">1</property>
                            <property name="adv.ps3_mode">0</property>
                            <property name="adv.qt_mode">0</property>
                            <property name="adv.transform_8x8">0</property>
                            <property name="adv.weighted_pred_flag">1</property>
                            <property name="alternateJava" isNull="true"/>
                            <property name="cadenceReEntryMode">0</property>
                            <property name="defaultInputPin">Video</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="detect2224Cadence">true</property>
                            <property name="detect22Cadence">true</property>
                            <property name="detect2332Cadence">true</property>
                            <property name="detect32322Cadence">true</property>
                            <property name="detect32Cadence">true</property>
                            <property name="detect55Cadence">true</property>
                            <property name="detect64Cadence">true</property>
                            <property name="detect87Cadence">true</property>
                            <property name="dominance">pass_through</property>
                            <property name="edge_smoothing" isNull="true"/>
                            <property name="filterControl">0</property>
                            <property name="filterType">0</property>
                            <property name="gen.enable_3d_encoding">false</property>
                            <property name="gen.level_idc">51</property>
                            <property name="gen.mbaff_mode">false</property>
                            <property name="gen.profile_idc">100</property>
                            <property name="gen.speed">0</property>
                            <property name="gen.sym_mode">1</property>
                            <property name="gop.adaptive_bframes">true</property>
                            <property name="gop.bframes">2</property>
                            <property name="gop.duration">2</property>
                            <property name="gop.idr_control">1</property>
                            <property name="gop.idr_period">1</property>
                            <property name="gop.min_intra_period">4</property>
                            <property name="gop.mode">0</property>
                            <property name="gop.sps_period">0</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="interpolation">CUBIC2P_BSPLINE</property>
                            <property name="javaSelector" isNull="true"/>
                            <property name="latency">30</property>
                            <property name="legacyDataTypeSerialization" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="me.max_refs">1</property>
                            <property name="me.search_range">-1</property>
                            <property name="me.subdiv">7</property>
                            <property name="mt.max_pict_tasks">-1</property>
                            <property name="mt.mode">2</property>
                            <property name="mt.num_threads">8</property>
                            <property name="noiseTolerance">0</property>
                            <property name="oopLaunchTimeoutSeconds" isNull="true"/>
                            <property name="outputFrameRate">matchInputFieldRate</property>
                            <property name="output_height" isNull="true"/>
                            <property name="output_width" isNull="true"/>
                            <property name="pluginOptions" isNull="true"/>
                            <property name="preset">yuyv</property>
                            <property name="pulldown_mode">2:3TFF</property>
                            <property name="rc.auto_qp">true</property>
                            <property name="rc.initial_cpb_removal_delay">-1</property>
                            <property name="rc.kbps">3500</property>
                            <property name="rc.max_intra_frame_bytes">0</property>
                            <property name="rc.max_kbps">0</property>
                            <property name="rc.min_intra_frame_bytes">0</property>
                            <property name="rc.qp_delta_b">4</property>
                            <property name="rc.qp_delta_p">2</property>
                            <property name="rc.qp_intra">30</property>
                            <property name="rc.qp_max">51</property>
                            <property name="rc.qp_min">8</property>
                            <property name="rc.scene_detect">35</property>
                            <property name="rc.type">2</property>
                            <property name="rc.unrestricted_iframepicsize">0</property>
                            <property name="rc.vbv_length">1000</property>
                            <property name="runOutOfProcess">if32bit</property>
                            <property name="sei.pic_timing_flag">1</property>
                            <property name="sei.split_sei_payload">0</property>
                            <property name="shift">shift_up</property>
                            <property name="showOutOfProcessGUI" isNull="true"/>
                            <property name="slice.deblock">1</property>
                            <property name="slice.mode">0</property>
                            <property name="slice.param">1</property>
                            <property name="start_timecode">00:00:00:00/30</property>
                            <property name="threads">1</property>
                            <property name="verticalBandwidthControl">1</property>
                            <property name="video_format">Same as Input(1tc/field)</property>
                            <property name="vui.bitstream_restriction_flag">false</property>
                            <property name="vui.colour_description_present_flag">false</property>
                            <property name="vui.colour_primaries">2</property>
                            <property name="vui.matrix_coeffients">2</property>
                            <property name="vui.max_bytes_per_pic_denom">false</property>
                            <property name="vui.nal_hrd_parameters_present_flag">false</property>
                            <property name="vui.pic_struct_present_flag">true</property>
                            <property name="vui.timing_info_present_flag">false</property>
                            <property name="vui.transfer_characteristics">2</property>
                            <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                            <property name="vui.video_format">2</property>
                            <property name="vui.video_signal_type_present_flag">true</property>
                            <componentName>AVC Video Encoder 3</componentName>
                            <componentDefinitionName>Advanced AVC Encoder</componentDefinitionName>
                            <componentDefinitionGuid>A3597472-D51E-44d9-9F0A-395744A83FA3</componentDefinitionGuid>
                            <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="Video" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                            <pin name="Timecode (SEI)" type="INPUT_IO"/>
                            <pin name="EIA-608 Captions" type="INPUT_IO"/>
                            <pin name="EIA-708 Captions" type="INPUT_IO">
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                                <property name="_graphDisplayLocation">-61,102</property>
                            </pin>
                            <pin name="User Data" type="INPUT_IO">
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                                <property name="_graphDisplayLocation">-57,133</property>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition displayName="Target Video Format" name="video_format" group="General" dynamic="true">
                                <initialValue>Same as Input(1tc/frame)</initialValue>
                                <valueType type="STRING">
                                    <valueRestriction strictEnum="true">
                                        <enumerationValue val="23.98i(1tc/frame)" displayName="23.98 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="23.98i(1tc/field)" displayName="23.98 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="24i(1tc/frame)" displayName="24 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="24i(1tc/field)" displayName="24 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="25i(1tc/frame)" displayName="25 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="25i(1tc/field)" displayName="25 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="29.97i(1tc/frame)" displayName="29.97 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="29.97i(1tc/field)" displayName="29.97 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="30i(1tc/frame)" displayName="30 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="30i(1tc/field)" displayName="30 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="50p" displayName="50 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="59.94p" displayName="59.94 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="60p" displayName="60 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="Same as Input(1tc/frame)" displayName="Input (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="Same as Input(1tc/field)" displayName="Input (inserted per field)"></enumerationValue>
                                    </valueRestriction>
                                </valueType>
                            </propertyDefinition>
                            <propertyDefinition displayName="Timecode Setting" name="start_timecode" group="General" dynamic="true">
                                <initialValue>00:00:00:00/30</initialValue>
                                <valueType type="TIMECODE"/>
                            </propertyDefinition>
                            <propertyDefinition displayName="Insert CGMS-A" name="insert-cgms-a" group="Copy Generation Management System (Analog)" dynamic="true">
                                <initialValue>FALSE</initialValue>
                                <valueType type="BOOLEAN"/>
                            </propertyDefinition>
                            <propertyDefinition displayName="CGMS-A Bit:" name="cgms-a-bit" group="Copy Generation Management System (Analog)" dynamic="true">
                                <initialValue>0</initialValue>
                                <valueType type="STRING">
                                    <valueRestriction strictEnum="true">
                                        <enumerationValue val="0" displayName="(0,0) Copying is permitted without restriction"></enumerationValue>
                                        <enumerationValue val="1" displayName="(0,1) Condition not to be used"></enumerationValue>
                                        <enumerationValue val="2" displayName="(1,0) One generation of copies may be made"></enumerationValue>
                                        <enumerationValue val="3" displayName="(1,1) No copying is permitted"></enumerationValue>
                                    </valueRestriction>
                                </valueType>
                            </propertyDefinition>
                            <propertyDefinition displayName="APS Bit:" name="aps-bit" group="Copy Generation Management System (Analog)" dynamic="true">
                                <initialValue>0</initialValue>
                                <valueType type="STRING">
                                    <valueRestriction strictEnum="true">
                                        <enumerationValue val="0" displayName="(0,0) No APS"></enumerationValue>
                                        <enumerationValue val="1" displayName="(0,1) PSP On; Split Burst Off"></enumerationValue>
                                        <enumerationValue val="2" displayName="(1,0) PSP On; 2 line Split Burst On"></enumerationValue>
                                        <enumerationValue val="3" displayName="(1,1) PSP On; 4 line Split Burst On"></enumerationValue>
                                    </valueRestriction>
                                </valueType>
                            </propertyDefinition>
                            <propertyDefinition displayName="Analog Source Bit:" name="analog-source-bit" group="Copy Generation Management System (Analog)" dynamic="true">
                                <initialValue>0</initialValue>
                                <valueType type="STRING">
                                    <valueRestriction strictEnum="true">
                                        <enumerationValue val="0"></enumerationValue>
                                        <enumerationValue val="1"></enumerationValue>
                                    </valueRestriction>
                                </valueType>
                            </propertyDefinition>
                            <propertyDefinition displayName="Insert Content Advisory (V-Chip Information)" name="insert-v-chip-info" group="Content Advisory (V-Chip Information)" dynamic="true">
                                <initialValue>FALSE</initialValue>
                                <valueType type="BOOLEAN"/>
                            </propertyDefinition>
                            <propertyDefinition displayName="System:" name="system" group="Content Advisory (V-Chip Information)" dynamic="true">
                                <initialValue>0</initialValue>
                                <valueType type="STRING">
                                    <valueRestriction strictEnum="true">
                                        <enumerationValue val="0" displayName="MPAA"></enumerationValue>
                                        <enumerationValue val="1" displayName="U.S. TV Parental Guidelines"></enumerationValue>
                                        <enumerationValue val="2" displayName="Canadian English Language Rating"></enumerationValue>
                                        <enumerationValue val="3" displayName="Canadian French Language Rating"></enumerationValue>
                                    </valueRestriction>
                                </valueType>
                            </propertyDefinition>
                            <propertyDefinition displayName="Rating:" name="rating" group="Content Advisory (V-Chip Information)" dynamic="true">
                                <valueType type="STRING">
                                    <valueRestriction strictEnum="true"/>
                                </valueType>
                            </propertyDefinition>
                            <propertyDefinition displayName="Fantasy Violence" name="fantasy-violence" group="Content Advisory (V-Chip Information)" dynamic="true">
                                <initialValue>FALSE</initialValue>
                                <valueType type="BOOLEAN"/>
                            </propertyDefinition>
                            <propertyDefinition displayName="Violence" name="violence" group="Content Advisory (V-Chip Information)" dynamic="true">
                                <initialValue>FALSE</initialValue>
                                <valueType type="BOOLEAN"/>
                            </propertyDefinition>
                            <propertyDefinition displayName="Sexual Situations" name="sexual-situations" group="Content Advisory (V-Chip Information)" dynamic="true">
                                <initialValue>FALSE</initialValue>
                                <valueType type="BOOLEAN"/>
                            </propertyDefinition>
                            <propertyDefinition displayName="Adult Language" name="adult-language" group="Content Advisory (V-Chip Information)" dynamic="true">
                                <initialValue>FALSE</initialValue>
                                <valueType type="BOOLEAN"/>
                            </propertyDefinition>
                            <propertyDefinition displayName="Sexually Suggestive Dialog" name="sexually-suggestive-dialog" group="Content Advisory (V-Chip Information)" dynamic="true">
                                <initialValue>FALSE</initialValue>
                                <valueType type="BOOLEAN"/>
                            </propertyDefinition>
                            <property name="Aspect Ratio" isNull="true"/>
                            <property name="Frame Rate" isNull="true"/>
                            <property name="General.extended_sar" isNull="true"/>
                            <property name="General.sar" isNull="true"/>
                            <property name="Timecode">none</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1389.1727600097656,167.73536682128906</property>
                            <property name="_graphMinDisplaySize">1052.0,214.0</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="_useSerializedDataTypeDefs" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="adult-language">FALSE</property>
                            <property name="adv.custom_frame_rate_timescale">240000</property>
                            <property name="adv.direct_mode">0</property>
                            <property name="adv.enable_custom_frame_rate_timescale">false</property>
                            <property name="adv.intra_precision">2</property>
                            <property name="adv.mbaff_scan_type">1</property>
                            <property name="adv.ps3_mode">0</property>
                            <property name="adv.qt_mode">0</property>
                            <property name="adv.transform_8x8">0</property>
                            <property name="adv.weighted_pred_flag">1</property>
                            <property name="alternateJava" isNull="true"/>
                            <property name="analog-source-bit">0</property>
                            <property name="aps-bit">0</property>
                            <property name="cadenceReEntryMode">0</property>
                            <property name="cgms-a-bit">0</property>
                            <property name="defaultInputPin">Video</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="detect2224Cadence">true</property>
                            <property name="detect22Cadence">true</property>
                            <property name="detect2332Cadence">true</property>
                            <property name="detect32322Cadence">true</property>
                            <property name="detect32Cadence">true</property>
                            <property name="detect55Cadence">true</property>
                            <property name="detect64Cadence">true</property>
                            <property name="detect87Cadence">true</property>
                            <property name="dominance">pass_through</property>
                            <property name="edge_smoothing" isNull="true"/>
                            <property name="fantasy-violence">FALSE</property>
                            <property name="filterControl">0</property>
                            <property name="filterType">0</property>
                            <property name="gen.enable_3d_encoding">false</property>
                            <property name="gen.level_idc">51</property>
                            <property name="gen.mbaff_mode">false</property>
                            <property name="gen.profile_idc">100</property>
                            <property name="gen.speed">0</property>
                            <property name="gen.sym_mode">1</property>
                            <property name="gop.adaptive_bframes">true</property>
                            <property name="gop.bframes">2</property>
                            <property name="gop.duration">2</property>
                            <property name="gop.idr_control">1</property>
                            <property name="gop.idr_period">1</property>
                            <property name="gop.min_intra_period">4</property>
                            <property name="gop.mode">0</property>
                            <property name="gop.sps_period">0</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="insert-cgms-a">FALSE</property>
                            <property name="insert-v-chip-info">FALSE</property>
                            <property name="interpolation">CUBIC2P_BSPLINE</property>
                            <property name="javaSelector" isNull="true"/>
                            <property name="latency">30</property>
                            <property name="legacyDataTypeSerialization" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="me.max_refs">1</property>
                            <property name="me.search_range">-1</property>
                            <property name="me.subdiv">7</property>
                            <property name="mt.max_pict_tasks">-1</property>
                            <property name="mt.mode">2</property>
                            <property name="mt.num_threads">8</property>
                            <property name="noiseTolerance">0</property>
                            <property name="oopLaunchTimeoutSeconds" isNull="true"/>
                            <property name="outputFrameRate">matchInputFieldRate</property>
                            <property name="output_height" isNull="true"/>
                            <property name="output_width" isNull="true"/>
                            <property name="pluginOptions" isNull="true"/>
                            <property name="preset">yuyv</property>
                            <property name="pulldown_mode">2:3TFF</property>
                            <property name="rating">0</property>
                            <property name="rc.auto_qp">true</property>
                            <property name="rc.initial_cpb_removal_delay">-1</property>
                            <property name="rc.kbps">6500</property>
                            <property name="rc.max_intra_frame_bytes">0</property>
                            <property name="rc.max_kbps">0</property>
                            <property name="rc.min_intra_frame_bytes">0</property>
                            <property name="rc.qp_delta_b">4</property>
                            <property name="rc.qp_delta_p">2</property>
                            <property name="rc.qp_intra">30</property>
                            <property name="rc.qp_max">51</property>
                            <property name="rc.qp_min">8</property>
                            <property name="rc.scene_detect">35</property>
                            <property name="rc.type">2</property>
                            <property name="rc.unrestricted_iframepicsize">0</property>
                            <property name="rc.vbv_length">1000</property>
                            <property name="runOutOfProcess">if32bit</property>
                            <property name="sei.pic_timing_flag">1</property>
                            <property name="sei.split_sei_payload">0</property>
                            <property name="sexual-situations">FALSE</property>
                            <property name="sexually-suggestive-dialog">FALSE</property>
                            <property name="shift">shift_up</property>
                            <property name="showOutOfProcessGUI" isNull="true"/>
                            <property name="slice.deblock">1</property>
                            <property name="slice.mode">0</property>
                            <property name="slice.param">1</property>
                            <property name="start_timecode">00:00:00:00/30</property>
                            <property name="system">0</property>
                            <property name="threads">1</property>
                            <property name="verticalBandwidthControl">1</property>
                            <property name="video_format">Same as Input(1tc/field)</property>
                            <property name="violence">FALSE</property>
                            <property name="vui.bitstream_restriction_flag">false</property>
                            <property name="vui.colour_description_present_flag">false</property>
                            <property name="vui.colour_primaries">2</property>
                            <property name="vui.matrix_coeffients">2</property>
                            <property name="vui.max_bytes_per_pic_denom">false</property>
                            <property name="vui.nal_hrd_parameters_present_flag">false</property>
                            <property name="vui.pic_struct_present_flag">true</property>
                            <property name="vui.timing_info_present_flag">false</property>
                            <property name="vui.transfer_characteristics">2</property>
                            <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                            <property name="vui.video_format">2</property>
                            <property name="vui.video_signal_type_present_flag">true</property>
                            <componentName>AVC Video Encoder 2</componentName>
                            <componentDefinitionName>Advanced AVC Encoder</componentDefinitionName>
                            <componentDefinitionGuid>A3597472-D51E-44d9-9F0A-395744A83FA3</componentDefinitionGuid>
                            <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="Video" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                            <pin name="Timecode (SEI)" type="INPUT_IO"/>
                            <pin name="EIA-608 Captions" type="INPUT_IO"/>
                            <pin name="EIA-708 Captions" type="INPUT_IO">
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                                <property name="_graphDisplayLocation">-61,102</property>
                            </pin>
                            <pin name="User Data" type="INPUT_IO">
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                                <property name="_graphDisplayLocation">-57,133</property>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition displayName="Target Video Format" name="video_format" group="General" dynamic="true">
                                <initialValue>Same as Input(1tc/frame)</initialValue>
                                <valueType type="STRING">
                                    <valueRestriction strictEnum="true">
                                        <enumerationValue val="23.98i(1tc/frame)" displayName="23.98 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="23.98i(1tc/field)" displayName="23.98 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="24i(1tc/frame)" displayName="24 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="24i(1tc/field)" displayName="24 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="25i(1tc/frame)" displayName="25 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="25i(1tc/field)" displayName="25 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="29.97i(1tc/frame)" displayName="29.97 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="29.97i(1tc/field)" displayName="29.97 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="30i(1tc/frame)" displayName="30 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="30i(1tc/field)" displayName="30 fps (inserted per field)"></enumerationValue>
                                        <enumerationValue val="50p" displayName="50 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="59.94p" displayName="59.94 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="60p" displayName="60 fps (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="Same as Input(1tc/frame)" displayName="Input (inserted per frame)"></enumerationValue>
                                        <enumerationValue val="Same as Input(1tc/field)" displayName="Input (inserted per field)"></enumerationValue>
                                    </valueRestriction>
                                </valueType>
                            </propertyDefinition>
                            <propertyDefinition displayName="Timecode Setting" name="start_timecode" group="General" dynamic="true">
                                <initialValue>00:00:00:00/30</initialValue>
                                <valueType type="TIMECODE"/>
                            </propertyDefinition>
                            <property name="Aspect Ratio" isNull="true"/>
                            <property name="Frame Rate" isNull="true"/>
                            <property name="General.extended_sar" isNull="true"/>
                            <property name="General.sar" isNull="true"/>
                            <property name="Timecode">none</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1389.1727600097656,526.7353668212891</property>
                            <property name="_graphMinDisplaySize">1052.0,214.0</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="_useSerializedDataTypeDefs" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="adv.custom_frame_rate_timescale">240000</property>
                            <property name="adv.direct_mode">0</property>
                            <property name="adv.enable_custom_frame_rate_timescale">false</property>
                            <property name="adv.intra_precision">2</property>
                            <property name="adv.mbaff_scan_type">1</property>
                            <property name="adv.ps3_mode">0</property>
                            <property name="adv.qt_mode">0</property>
                            <property name="adv.transform_8x8">0</property>
                            <property name="adv.weighted_pred_flag">1</property>
                            <property name="alternateJava" isNull="true"/>
                            <property name="cadenceReEntryMode">0</property>
                            <property name="defaultInputPin">Video</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="detect2224Cadence">true</property>
                            <property name="detect22Cadence">true</property>
                            <property name="detect2332Cadence">true</property>
                            <property name="detect32322Cadence">true</property>
                            <property name="detect32Cadence">true</property>
                            <property name="detect55Cadence">true</property>
                            <property name="detect64Cadence">true</property>
                            <property name="detect87Cadence">true</property>
                            <property name="dominance">pass_through</property>
                            <property name="edge_smoothing" isNull="true"/>
                            <property name="filterControl">0</property>
                            <property name="filterType">0</property>
                            <property name="gen.enable_3d_encoding">false</property>
                            <property name="gen.level_idc">51</property>
                            <property name="gen.mbaff_mode">false</property>
                            <property name="gen.profile_idc">100</property>
                            <property name="gen.speed">0</property>
                            <property name="gen.sym_mode">1</property>
                            <property name="gop.adaptive_bframes">true</property>
                            <property name="gop.bframes">2</property>
                            <property name="gop.duration">2</property>
                            <property name="gop.idr_control">1</property>
                            <property name="gop.idr_period">1</property>
                            <property name="gop.min_intra_period">4</property>
                            <property name="gop.mode">0</property>
                            <property name="gop.sps_period">0</property>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="interpolation">CUBIC2P_BSPLINE</property>
                            <property name="javaSelector" isNull="true"/>
                            <property name="latency">30</property>
                            <property name="legacyDataTypeSerialization" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="me.max_refs">1</property>
                            <property name="me.search_range">-1</property>
                            <property name="me.subdiv">7</property>
                            <property name="mt.max_pict_tasks">-1</property>
                            <property name="mt.mode">2</property>
                            <property name="mt.num_threads">8</property>
                            <property name="noiseTolerance">0</property>
                            <property name="oopLaunchTimeoutSeconds" isNull="true"/>
                            <property name="outputFrameRate">matchInputFieldRate</property>
                            <property name="output_height" isNull="true"/>
                            <property name="output_width" isNull="true"/>
                            <property name="pluginOptions" isNull="true"/>
                            <property name="preset">yuyv</property>
                            <property name="pulldown_mode">2:3TFF</property>
                            <property name="rc.auto_qp">true</property>
                            <property name="rc.initial_cpb_removal_delay">-1</property>
                            <property name="rc.kbps">2200</property>
                            <property name="rc.max_intra_frame_bytes">0</property>
                            <property name="rc.max_kbps">0</property>
                            <property name="rc.min_intra_frame_bytes">0</property>
                            <property name="rc.qp_delta_b">4</property>
                            <property name="rc.qp_delta_p">2</property>
                            <property name="rc.qp_intra">30</property>
                            <property name="rc.qp_max">51</property>
                            <property name="rc.qp_min">8</property>
                            <property name="rc.scene_detect">35</property>
                            <property name="rc.type">2</property>
                            <property name="rc.unrestricted_iframepicsize">0</property>
                            <property name="rc.vbv_length">1000</property>
                            <property name="runOutOfProcess">if32bit</property>
                            <property name="sei.pic_timing_flag">1</property>
                            <property name="sei.split_sei_payload">0</property>
                            <property name="shift">shift_up</property>
                            <property name="showOutOfProcessGUI" isNull="true"/>
                            <property name="slice.deblock">1</property>
                            <property name="slice.mode">0</property>
                            <property name="slice.param">1</property>
                            <property name="start_timecode">00:00:00:00/30</property>
                            <property name="threads">1</property>
                            <property name="verticalBandwidthControl">1</property>
                            <property name="video_format">Same as Input(1tc/field)</property>
                            <property name="vui.bitstream_restriction_flag">false</property>
                            <property name="vui.colour_description_present_flag">false</property>
                            <property name="vui.colour_primaries">2</property>
                            <property name="vui.matrix_coeffients">2</property>
                            <property name="vui.max_bytes_per_pic_denom">false</property>
                            <property name="vui.nal_hrd_parameters_present_flag">false</property>
                            <property name="vui.pic_struct_present_flag">true</property>
                            <property name="vui.timing_info_present_flag">false</property>
                            <property name="vui.transfer_characteristics">2</property>
                            <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                            <property name="vui.video_format">2</property>
                            <property name="vui.video_signal_type_present_flag">true</property>
                            <componentName>AVC Video Encoder 4</componentName>
                            <componentDefinitionName>Advanced AVC Encoder</componentDefinitionName>
                            <componentDefinitionGuid>A3597472-D51E-44d9-9F0A-395744A83FA3</componentDefinitionGuid>
                            <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="Video" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                            <pin name="Timecode (SEI)" type="INPUT_IO"/>
                            <pin name="EIA-608 Captions" type="INPUT_IO"/>
                            <pin name="EIA-708 Captions" type="INPUT_IO">
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                                <property name="_graphDisplayLocation">-61,102</property>
                            </pin>
                            <pin name="User Data" type="INPUT_IO">
                                <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                                <property name="_graphDisplayLocation">-57,133</property>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">1843.0001220703125,191.91827392578125</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in1</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Branch Merger</componentName>
                            <componentDefinitionName>BranchMerger</componentDefinitionName>
                            <componentDefinitionGuid>5442b6d8-5672-430f-91ce-5b047e84bd7f</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in1" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                            <pin name="in2" type="INPUT_PUSH">
                                <pinDefinition name="in2" displayName="In 2" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in3" type="INPUT_PUSH">
                                <pinDefinition name="in3" displayName="In 3" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in4" type="INPUT_PUSH">
                                <pinDefinition name="in4" displayName="In 4" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="in5" type="INPUT_PUSH">
                                <pinDefinition name="in5" displayName="In 5" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">746.0001220703125,46.91827392578125</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Gate</componentName>
                            <componentDefinitionName>Logic Gate</componentDefinitionName>
                            <componentDefinitionGuid>1824FAE2-9B42-4bb1-89D8-41104BC1B76C</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="ingate" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">975.0001220703125,256.91827392578125</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Gate 2</componentName>
                            <componentDefinitionName>Logic Gate</componentDefinitionName>
                            <componentDefinitionGuid>1824FAE2-9B42-4bb1-89D8-41104BC1B76C</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="ingate" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">1158.0001220703125,406.91827392578125</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Gate 3</componentName>
                            <componentDefinitionName>Logic Gate</componentDefinitionName>
                            <componentDefinitionGuid>1824FAE2-9B42-4bb1-89D8-41104BC1B76C</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="ingate" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">1214.0001220703125,534.9182739257812</property>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <componentName>Logic Gate 4</componentName>
                            <componentDefinitionName>Logic Gate</componentDefinitionName>
                            <componentDefinitionGuid>1824FAE2-9B42-4bb1-89D8-41104BC1B76C</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="ingate" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                    </childComponents>
                    <pin name="in" type="INPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">560.0,173.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="in" displayName="Video" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Video Out" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">1874.2286376953125,73.51223754882812</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="Video Out" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="608 Captions" type="INPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">105.56582641601562,215.66909790039062</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="608 Captions" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1549.4613037109375,57.08172607421875</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>ISO MPEG-4 Multiplexer</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 3" type="INPUT_IO">
                        <pinDefinition name="Track 3" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1845.4776611328125,57.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_blueprintStartDateYYYYMMDD" path="../../blueprintStartDateYYYYMMDD"/&gt;
    &lt;propertyBinding variable="ROOT_blueprintStartTimeFilenameSafe" path="../../blueprintStartTimeFilenameSafe"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_blueprintStartDateYYYYMMDD}_${ROOT_blueprintStartTimeFilenameSafe}.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_blueprintStartDateYYYYMMDD}_${ROOT_blueprintStartTimeFilenameSafe}.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1852.4776611328125,451.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_blueprintStartDateYYYYMMDD" path="../../blueprintStartDateYYYYMMDD"/&gt;
    &lt;propertyBinding variable="ROOT_blueprintStartTimeFilenameSafe" path="../../blueprintStartTimeFilenameSafe"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_blueprintStartDateYYYYMMDD}_${ROOT_blueprintStartTimeFilenameSafe}.ttml"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_blueprintStartDateYYYYMMDD}_${ROOT_blueprintStartTimeFilenameSafe}.ttml"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output 2</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="false" displayName="Play List Name" name="Playlist 1.playlist_name" description="Play List Name" group="Playlist 1" dynamic="true">
                        <valueType type="STRING"/>
                    </propertyDefinition>
                    <propertyDefinition hidden="false" displayName="Play List Path" name="Playlist 1.playlist_path" description="Play List Path" group="Playlist 1" dynamic="true">
                        <valueType type="STRING"/>
                    </propertyDefinition>
                    <propertyDefinition hidden="false" displayName="Play List Retrieval Path" name="Playlist 1.playlist_retrieval_path" description="Play List Retrieval Path" group="Playlist 1" dynamic="true">
                        <valueType type="STRING"/>
                    </propertyDefinition>
                    <propertyDefinition hidden="false" displayName="Play List Name" name="Playlist 2.playlist_name" description="Play List Name" group="Playlist 2" dynamic="true">
                        <valueType type="STRING"/>
                    </propertyDefinition>
                    <propertyDefinition hidden="false" displayName="Play List Path" name="Playlist 2.playlist_path" description="Play List Path" group="Playlist 2" dynamic="true">
                        <valueType type="STRING"/>
                    </propertyDefinition>
                    <propertyDefinition hidden="false" displayName="Play List Retrieval Path" name="Playlist 2.playlist_retrieval_path" description="Play List Retrieval Path" group="Playlist 2" dynamic="true">
                        <valueType type="STRING"/>
                    </propertyDefinition>
                    <property name="Playlist 1.playlist_name" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_blueprintStartDateYYYYMMDD" path="../blueprintStartDateYYYYMMDD"/&gt;
    &lt;propertyBinding variable="ROOT_blueprintStartTimeFilenameSafe" path="../blueprintStartTimeFilenameSafe"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_sourceFileBaseName}_${ROOT_blueprintStartDateYYYYMMDD}_${ROOT_blueprintStartTimeFilenameSafe}.ism"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_sourceFileBaseName}_${ROOT_blueprintStartDateYYYYMMDD}_${ROOT_blueprintStartTimeFilenameSafe}.ism"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="Playlist 1.playlist_path" isNull="true"/>
                    <property name="Playlist 1.playlist_retrieval_path" isNull="true"/>
                    <property name="Playlist 2.playlist_name" isNull="true"/>
                    <property name="Playlist 2.playlist_path" isNull="true"/>
                    <property name="Playlist 2.playlist_retrieval_path" isNull="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">2125.0,238.0</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="base_path" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../outputWriteDirectory"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="base_retrieval_path" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="enable_authentication">false</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="mode">live</property>
                    <property name="password" isNull="true"/>
                    <property name="playlist 0.playlist_name" isNull="true"/>
                    <property name="playlist 0.playlist_path" isNull="true"/>
                    <property name="playlist 0.playlist_retrieval_path" isNull="true"/>
                    <property name="query_salt" isNull="true"/>
                    <property name="query_value" isNull="true"/>
                    <property name="segments_before_playback">3</property>
                    <property name="server_type">local_network</property>
                    <property name="target_duration">10</property>
                    <property name="upload_using">put</property>
                    <property name="user_name" isNull="true"/>
                    <componentName>SMIL Playlists Writer</componentName>
                    <componentDefinitionName>SMIL Playlists Writer</componentDefinitionName>
                    <componentDefinitionGuid>BC11DDED-63C0-4488-A7DB-309322189918</componentDefinitionGuid>
                    <componentOwningPluginName>SMILUtilities</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.SMILUtilities</componentOwningPluginId>
                    <childComponents/>
                    <pin name="writeComplete" type="EVENT"/>
                    <pin name="out" type="EVENT"/>
                    <pin name="Playlist 1" type="INPUT_PUSH">
                        <pinDefinition name="Playlist 1" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Playlist 2" type="INPUT_PUSH">
                        <pinDefinition name="Playlist 2" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                </component>
            </childComponents>
            <pin name="primarySourceFile" type="PROPERTY">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">8.0,10.0</property>
                <property name="_pinProperty">primarySourceFile</property>
            </pin>
            <pin name="clipListXml" type="PROPERTY">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">8.0,40.0</property>
                <property name="_pinProperty">clipListXml</property>
            </pin>
            <pin name="outputAssetsCommand" type="COMMAND">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">2558.89990234375,261.3812561035156</property>
            </pin>
        </component>
    </components>
    <pinConnections>
        <connection>
            <sourcePath>primarySourceFile</sourcePath>
            <destinationPath>Media File Input/filename</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output 2/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>SMIL Playlists Writer/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Stream Interleaver/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Input</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio Stream Interleaver/in 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedAudio 2</sourcePath>
            <destinationPath>Audio Stream Interleaver/in 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedAudio 3</sourcePath>
            <destinationPath>Audio Stream Interleaver/in 3</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedAudio 4</sourcePath>
            <destinationPath>Audio Stream Interleaver/in 4</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedAudio 5</sourcePath>
            <destinationPath>Audio Stream Interleaver/in 5</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedAudio 6</sourcePath>
            <destinationPath>Audio Stream Interleaver/in 6</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedVideo</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/in</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/Video Out</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/Video Out</sourcePath>
            <destinationPath>Video/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Branch Merger/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /out</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /in</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Logic Branch/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Video Data Type Updater - assume HD/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Branch Merger/in1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Video Data Type Updater - assume SD/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Branch Merger/in2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Logic Branch/yes</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Video Data Type Updater - assume HD/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Logic Branch/no</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing Colorspace or Aspect Ratio metadata update
Based on Framesize /Video Data Type Updater - assume SD/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger 3/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /out</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /in</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch/yes</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Video Data Type Updater - Assume Interlaced BFF/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Video Data Type Updater - Assume Interlaced BFF/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger 3/in2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch/no</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Video Data Type Updater - Assume Interlaced TFF/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Video Data Type Updater - Assume Interlaced TFF/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger 3/in3</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Video Data Type Updater - Assume Progressive/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger 3/in1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Video Data Type Updater - Assume Progressive/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch 2/yes</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger/in1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch 4/yes</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger/in2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch 3/yes</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch 4/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch 4/no</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger 2/in1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger 2/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch 3/no</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Branch Merger 2/in2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch 2/no</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Missing scan type (frame layout) metadata update
Based on framerates and framesize /Logic Branch 3/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Branch Merger/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/out</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/in</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Logic Branch/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Logic Branch/yes</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Branch Merger/in1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Deinterlacer/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Branch Merger/in2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Logic Branch/no</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Video Format Converter/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Video Format Converter/out</sourcePath>
            <destinationPath>Missing Metadata Update and Deinterlacing/LOGIC 
Deinterlace only if necessary/Deinterlacer/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/Data608Service</sourcePath>
            <destinationPath>EIA-608 Captions Selector/in 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>EIA-608 Captions Selector/out</sourcePath>
            <destinationPath>EIA-608 Captions Decoder/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>EIA-608 Captions Selector/out</sourcePath>
            <destinationPath>Video/608 Captions</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/Data608Service 3</sourcePath>
            <destinationPath>EIA-608 Captions Selector/in 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/Data608Service 2</sourcePath>
            <destinationPath>EIA-608 Captions Selector/in 3</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/Data608Service 4</sourcePath>
            <destinationPath>EIA-608 Captions Selector/in 4</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Input</sourcePath>
            <destinationPath>Audio Logic and Encoding/Logic Branch/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Branch Merger 2/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/out</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer/Track 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Video Input</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Video Input</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedVideo</sourcePath>
            <destinationPath>Audio Logic and Encoding/Video Input</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch/yes</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Data Type Updater/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Data Type Updater/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Down Mix 5.1 to Stereo/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch/no</sourcePath>
            <destinationPath>Audio Logic and Encoding/Logic Branch 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch 2/yes</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Data Type Updater 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch 2/no</sourcePath>
            <destinationPath>Audio Logic and Encoding/Logic Branch 3/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch 3/yes</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Data Type Updater 3/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch 3/no</sourcePath>
            <destinationPath>Audio Logic and Encoding/Logic Branch 4/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Data Type Updater 2/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Branch Merger/in2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Down Mix 5.1 to Stereo/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Branch Merger/in1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Data Type Updater 3/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Branch Merger/in3</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch 4/yes</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Data Type Updater 4/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Data Type Updater 4/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Branch Merger/in4</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch 4/no</sourcePath>
            <destinationPath>Audio Logic and Encoding/Logic Branch 5/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch 5/yes</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Data Type Updater 5/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch 5/no</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Stream Interleaver/in 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Logic Branch 5/no</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Stream Interleaver/in 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Data Type Updater 5/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Branch Merger/in5</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Branch Merger/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Input</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Data Type Updater 6/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Branch Merger/in6</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Out - 1080</sourcePath>
            <destinationPath>Audio Logic and Encoding/AAC Encoder - 192kbps/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/AAC Encoder - 192kbps/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Branch Merger 2/in2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Video Input</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Branch/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Input</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Gate/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Input</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Gate 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Input</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Gate 2 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Gate/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Out - 4K</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Out - 4K</sourcePath>
            <destinationPath>Audio Logic and Encoding/AAC Encoder - 256kbps/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Gate 2/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Out - 1080</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Gate 2 2/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Out - 1280 and less</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Audio Out - 1280 and less</sourcePath>
            <destinationPath>Audio Logic and Encoding/AAC Encoder - 128kbps/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Branch/yes</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Gate/ingate</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Branch/no</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Branch 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Branch 2/yes</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Gate 2/ingate</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Branch 2/no</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Branch 2 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Branch 2 2/yes</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Routing - based on Source Video Framesize/Logic Gate 2 2/ingate</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/AAC Encoder - 128kbps/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Branch Merger 2/in3</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/AAC Encoder - 256kbps/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Branch Merger 2/in1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Logic and Encoding/Audio Stream Interleaver/out</sourcePath>
            <destinationPath>Audio Logic and Encoding/Audio Data Type Updater 6/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>EIA-608 Captions Decoder/out_cc1</sourcePath>
            <destinationPath>Timed Text Selector/in 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>EIA-608 Captions Decoder/out_cc2</sourcePath>
            <destinationPath>Timed Text Selector/in 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>EIA-608 Captions Decoder/out_cc3</sourcePath>
            <destinationPath>Timed Text Selector/in 3</destinationPath>
        </connection>
        <connection>
            <sourcePath>EIA-608 Captions Decoder/out_cc4</sourcePath>
            <destinationPath>Timed Text Selector/in 4</destinationPath>
        </connection>
        <connection>
            <sourcePath>Timed Text Selector/out</sourcePath>
            <destinationPath>Language Code Updater/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Language Code Updater/out</sourcePath>
            <destinationPath>TTML Encoder/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>TTML Encoder/out</sourcePath>
            <destinationPath>File Output 2/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/in</sourcePath>
            <destinationPath>Video/Logic Branch/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Video Out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Branch Merger/out</sourcePath>
            <destinationPath>Video/Video Out</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/608 Captions</sourcePath>
            <destinationPath>Video/Logic Gate/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/608 Captions</sourcePath>
            <destinationPath>Video/Logic Gate 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/608 Captions</sourcePath>
            <destinationPath>Video/Logic Gate 3/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/608 Captions</sourcePath>
            <destinationPath>Video/Logic Gate 4/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch/yes</sourcePath>
            <destinationPath>Video/AVC Video Encoder/Video</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch/yes</sourcePath>
            <destinationPath>Video/Logic Gate/ingate</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch/no</sourcePath>
            <destinationPath>Video/Logic Branch 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch 2/yes</sourcePath>
            <destinationPath>Video/AVC Video Encoder 2/Video</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch 2/yes</sourcePath>
            <destinationPath>Video/Logic Gate 2/ingate</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch 2/no</sourcePath>
            <destinationPath>Video/Logic Branch 2 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch 2 2/yes</sourcePath>
            <destinationPath>Video/AVC Video Encoder 3/Video</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch 2 2/yes</sourcePath>
            <destinationPath>Video/Logic Gate 3/ingate</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch 2 2/no</sourcePath>
            <destinationPath>Video/Logic Branch 2 2 2/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/AVC Video Encoder/out</sourcePath>
            <destinationPath>Video/Branch Merger/in4</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Gate/out</sourcePath>
            <destinationPath>Video/AVC Video Encoder/EIA-608 Captions</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch 2 2 2/yes</sourcePath>
            <destinationPath>Video/AVC Video Encoder 4/Video</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Branch 2 2 2/yes</sourcePath>
            <destinationPath>Video/Logic Gate 4/ingate</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/AVC Video Encoder 3/out</sourcePath>
            <destinationPath>Video/Branch Merger/in2</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Gate 3/out</sourcePath>
            <destinationPath>Video/AVC Video Encoder 3/EIA-608 Captions</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/AVC Video Encoder 2/out</sourcePath>
            <destinationPath>Video/Branch Merger/in3</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Gate 2/out</sourcePath>
            <destinationPath>Video/AVC Video Encoder 2/EIA-608 Captions</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/AVC Video Encoder 4/out</sourcePath>
            <destinationPath>Video/Branch Merger/in1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video/Logic Gate 4/out</sourcePath>
            <destinationPath>Video/AVC Video Encoder 4/EIA-608 Captions</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer/mp4</sourcePath>
            <destinationPath>File Output/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output/writeComplete</sourcePath>
            <destinationPath>SMIL Playlists Writer/Playlist 1</destinationPath>
        </connection>
    </pinConnections>
    <authoringInfo>
        <kayakFrameworkVersion>1.3.7.1</kayakFrameworkVersion>
        <userName>Darren</userName>
        <userLanguage>en</userLanguage>
        <platform>Windows</platform>
        <osName>Windows 7</osName>
        <osArch>amd64</osArch>
        <osVersion>6.1</osVersion>
        <authoredDate>2014-12-08T15:59:13.357-05:00</authoredDate>
        <plugins>
            <plugin pluginId="ca.digitalrapids.AACSourceController" name="AAC Source Controller">
                <pluginVersion>1.0.24.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-04-14 15:07:54-0400</buildDate>
                    <checksum>b222e9b8c8a949a47a1e02bb9a195755</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AC3SourceController" name="AC3 Source Controller">
                <pluginVersion>1.0.28.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-05 15:15:06-0400</buildDate>
                    <checksum>62e71f1beeef7aece22de090ba387568</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AES3AudioProcessor" name="AES3AudioProcessor">
                <pluginVersion>1.0.17.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-21 10:18:34-0400</buildDate>
                    <checksum>ecd1dbfbdf4b5cc66ebfe984c4710466</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AES3SourceController" name="AES3SourceController">
                <pluginVersion>1.0.22.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-18 14:51:13-0400</buildDate>
                    <checksum>b3f12b371c965d668d8319a9119403b7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AVCSourceController" name="AVC Source Controller">
                <pluginVersion>1.0.41.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-20 16:41:09-0400</buildDate>
                    <checksum>f4141437f06529b2a3d69216ed1a5a23</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.Assets" name="Assets">
                <pluginVersion>1.0.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-18 16:43:49-0400</buildDate>
                    <checksum>255263cf15e1f4f2eedd6b3818774c5e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AudioFormatConverter" name="AudioFormatConverter">
                <pluginVersion>1.0.15.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-05 06:46:46-0400</buildDate>
                    <checksum>9600f0c46dbee32ddb35457459e78965</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AudioFormatUtilities" name="AudioFormatUtilities">
                <pluginVersion>1.0.66.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-29 15:37:46-0400</buildDate>
                    <checksum>9705e4a64a38cca30806aaf4971e1043</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CC608Decoder" name="CC608Decoder">
                <pluginVersion>1.0.30.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-05 06:48:28-0400</buildDate>
                    <checksum>6a17c59dfc34f9ef5cba3bb1f6c964f1</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="ClosedCaptionsUtilities">
                <pluginVersion>1.0.36.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-04 22:02:16-0400</buildDate>
                    <checksum>fe6129d46cd0265e7aaa539965c72caf</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAAC" name="CommonAAC">
                <pluginVersion>1.0.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-17 15:15:13-0400</buildDate>
                    <checksum>ac4d12dbec71f08576aebabfd714e796</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAC3" name="CommonAC3">
                <pluginVersion>1.1.9.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-17 15:19:06-0400</buildDate>
                    <checksum>5d89d2d67175f9f780957eb87758fdb9</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAES3" name="CommonAES3">
                <pluginVersion>1.1.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-07 16:36:41-0400</buildDate>
                    <checksum>a2e5b5f10dccddeebbf46747cfc4b273</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAVC" name="CommonAVC">
                <pluginVersion>1.0.19.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-25 08:30:23-0400</buildDate>
                    <checksum>fcb0f15f2ff405925f2482ad19995b63</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonComponents" name="CommonComponents">
                <pluginVersion>1.1.42.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-01 16:18:33-0400</buildDate>
                    <checksum>4f3a3a8519a193083a3538ea3b2f3419</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonDTS" name="CommonDTS">
                <pluginVersion>1.0.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-18 15:01:07-0400</buildDate>
                    <checksum>61f3136c4769493132791ab20eec14d2</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonDV" name="CommonDV">
                <pluginVersion>1.0.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-18 15:06:45-0400</buildDate>
                    <checksum>0ef596aedc812db9e887d54b06211d7b</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonDolbyE" name="CommonDolbyE">
                <pluginVersion>1.0.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-18 14:48:14-0400</buildDate>
                    <checksum>8d9db933132fff45f77d1cca159caca3</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonEAC3" name="CommonEAC3">
                <pluginVersion>1.0.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-18 15:10:24-0400</buildDate>
                    <checksum>4c4b4e50362a99bd36be9cb33d8a90ea</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonFont" name="CommonFont">
                <pluginVersion>1.0.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-02-21 19:27:37-0500</buildDate>
                    <checksum>385df375fa90cde4af26294936172be7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonImageFormats" name="CommonImageFormats">
                <pluginVersion>1.0.20.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-27 18:24:43-0500</buildDate>
                    <checksum>0a750fef4f8d6198f52719540c553974</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonIntelIPP" name="CommonIntelIPP">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-18 15:39:11-0400</buildDate>
                    <checksum>e6f3740379a0221f52a423b40710dc08</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonJ2KVideo" name="CommonJ2KVideo">
                <pluginVersion>1.0.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 08:10:17-0400</buildDate>
                    <checksum>bdf83e122eb86d00a3aeb8e42ff70fa2</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonLanguage" name="CommonLanguage">
                <pluginVersion>1.0.16.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-04 21:58:14-0400</buildDate>
                    <checksum>d7c19cf7c0d9bc0762ad70b66dee7eb5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG" name="CommonMPEG">
                <pluginVersion>1.0.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 13:13:48-0400</buildDate>
                    <checksum>a934a85eb4d0c86a757d80a792ae032c</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG1" name="CommonMPEG1">
                <pluginVersion>1.0.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 13:16:50-0400</buildDate>
                    <checksum>205521ae64fe69e65ef74070b07d321e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG2" name="CommonMPEG2">
                <pluginVersion>1.0.14.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-06 13:58:27-0400</buildDate>
                    <checksum>29f0e81dcbd0d42b41e514d90009b6bd</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG4" name="CommonMPEG4">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 13:26:02-0400</buildDate>
                    <checksum>ece896a89240b90c772bf89fb8179658</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMXF" name="CommonMXF">
                <pluginVersion>1.0.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 13:28:57-0400</buildDate>
                    <checksum>7cb12460dc147b18de8b15254e47df2a</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMedia" name="CommonMedia">
                <pluginVersion>1.0.57.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-07 16:36:29-0400</buildDate>
                    <checksum>eadf65d8ecd33d69316395a7edbd8f9e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMediaEncryption" name="CommonMediaEncryption">
                <pluginVersion>1.0.7.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-27 17:24:40-0400</buildDate>
                    <checksum>e8137cd8314ceb3134bd2569645d0cd2</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMetadata" name="CommonMetadata">
                <pluginVersion>1.0.5.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 12:54:21-0400</buildDate>
                    <checksum>69fc09d4a2979ccfd562a4ef693a60d7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonPlayReadyEncryption" name="CommonPlayReadyEncryption">
                <pluginVersion>1.0.5.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 13:59:20-0400</buildDate>
                    <checksum>f612be0829038b2d63b80d948dff3ee8</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonQuickTime" name="CommonQuickTime">
                <pluginVersion>1.0.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 15:35:15-0400</buildDate>
                    <checksum>2f8c5ead8bfb9e7f614f6c9ae9684166</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonStereoVideo" name="CommonStereoVideo">
                <pluginVersion>1.0.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 15:48:17-0400</buildDate>
                    <checksum>f1f78354a42e6e4a6a3aa17fa3be846f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonSubtitles" name="CommonSubtitles">
                <pluginVersion>1.0.10.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-04-21 17:41:29-0400</buildDate>
                    <checksum>5349794c466bcbd4bb5ef0b7940fe0c2</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonTimecode" name="CommonTimecode">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-06 15:04:52-0500</buildDate>
                    <checksum>0cdd77e7773cb540efb3cfc8396ca377</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonUltraviolet" name="CommonUltraviolet">
                <pluginVersion>1.0.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-03-25 11:15:24-0400</buildDate>
                    <checksum>0fbaf01cd371c81f4fcb7cf66f41f6d9</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonVC3" name="CommonVC3">
                <pluginVersion>1.0.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-06 15:27:08-0500</buildDate>
                    <checksum>ada6c958950fadf2780b72caac6bd90c</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonVideoSystem" name="CommonVideoSystem">
                <pluginVersion>1.0.52.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-04-01 13:53:08-0400</buildDate>
                    <checksum>9854595f8a2afa2d07eb5cf1092414d4</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonWAVE" name="CommonWAVE">
                <pluginVersion>1.0.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 17:02:27-0400</buildDate>
                    <checksum>df370b1fb4c0a1c420d8c1385c428223</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRAVCEncoder" name="DRAVCEncoder">
                <pluginVersion>1.0.82.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-25 08:32:06-0400</buildDate>
                    <checksum>d37092eb8d015396a7082a4117756f91</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRColorspaceConverter" name="DRColorspaceConverter">
                <pluginVersion>1.2.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-05-23 09:38:41-0400</buildDate>
                    <checksum>8661380f60443341bff9477a31fe264f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRDeinterlacer" name="DRDeinterlacer">
                <pluginVersion>1.4.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-06 15:02:19-0400</buildDate>
                    <checksum>5b7d25a750a9896f4e97321b79dd215e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRMediaProcessing" name="DRMediaProcessing">
                <pluginVersion>2.5.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-15 16:26:44-0400</buildDate>
                    <checksum>a0fe144ff3d06f6413126c08240bd42f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRProgressiveToInterlaced" name="DRProgressiveToInterlaced">
                <pluginVersion>1.1.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-06 15:16:49-0400</buildDate>
                    <checksum>3b980be7bfcfb666ec214c65afc42b5f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRScaler" name="DRScaler">
                <pluginVersion>1.2.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-06 15:40:19-0400</buildDate>
                    <checksum>a11ff459fd62b17711259e799f8c8e3d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DTSSourceController" name="DTS Source Controller">
                <pluginVersion>1.0.12.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-01 15:22:20-0400</buildDate>
                    <checksum>374aaf0ece6f5fecefc8c73de24a6f3e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DirectShowFileSource" name="DirectShowFileSource">
                <pluginVersion>1.0.19.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-20 21:17:46-0500</buildDate>
                    <checksum>e35fae50cdcf706cb794dd1bd14b35cf</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DolbyESourceController" name="Dolby E Source Controller">
                <pluginVersion>1.0.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-07 16:37:41-0400</buildDate>
                    <checksum>857e18a3b26edee7a84f8eae5f741178</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DolbyPulseEncoder" name="DolbyPulseEncoder">
                <pluginVersion>1.0.30.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-07 16:42:48-0400</buildDate>
                    <checksum>e15cba882effe4b96d06111bf86ef037</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.EAC3SourceController" name="EAC3 Source Controller">
                <pluginVersion>1.0.12.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-01 15:50:54-0400</buildDate>
                    <checksum>c237507b50e8d6842ffb8e24af37a314</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.EIACaptionsRetimer" name="EIACaptionsRetimer">
                <pluginVersion>1.0.15.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-04 22:04:06-0400</buildDate>
                    <checksum>a8b1b125c02afb734b2bee78ac3f04a4</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.HttpUtilities" name="HTTP Utilities">
                <pluginVersion>1.2.22.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-05 06:46:22-0400</buildDate>
                    <checksum>dc54776f1c4464b6228801ad4169d9c5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.KayakCore" name="KayakCore">
                <pluginVersion>1.3.7.1</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-18 15:58:18-0500</buildDate>
                    <checksum>a6fcff68bae2ff9865525c820e4a8937</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.KayakDesigner" name="KayakDesigner">
                <pluginVersion>1.3.7.1</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-18 16:54:18-0500</buildDate>
                    <checksum>38b1a0666781ebd5bd12a51f94098a84</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2AudioSourceController" name="MPEG2AudioSourceController">
                <pluginVersion>1.0.27.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-06 11:19:39-0400</buildDate>
                    <checksum>113876451453065f6475052338cbd1bc</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2UDDemuxer">
                <pluginVersion>1.1.24.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-05-26 15:16:23-0400</buildDate>
                    <checksum>5ec641a119f018ac0572e7d8143f8562</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2UDMuxer" name="MPEG2UDMuxer">
                <pluginVersion>1.0.26.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-23 15:54:46-0400</buildDate>
                    <checksum>ffc0f3083cd69dc13b4923dc343b9208</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2VideoSourceController" name="MPEG2VideoSourceController">
                <pluginVersion>1.0.24.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-31 09:14:36-0400</buildDate>
                    <checksum>5020a7167ae47cb1c1c648bb53f2d8c8</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG4Muxer" name="MPEG4Muxer">
                <pluginVersion>1.1.58.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-07 13:56:50-0400</buildDate>
                    <checksum>1871d8f170ad6194b499611175662bab</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEGVideoDecoder" name="MPEGVideoDecoder">
                <pluginVersion>1.0.18.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-29 09:55:42-0400</buildDate>
                    <checksum>2f007147892731f873236a49c704358f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MXFDemuxer" name="MXFDemuxer">
                <pluginVersion>1.0.46.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-26 13:20:07-0400</buildDate>
                    <checksum>a77dd94af3f4525030b7e8a0005b2be5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaInspection" name="MediaInspection">
                <pluginVersion>1.0.42.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-11 12:48:41-0400</buildDate>
                    <checksum>ff66be769001ae9abfc6a170744e7a67</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaManager" name="Media Manager">
                <pluginVersion>1.0.49.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-12 15:34:14-0400</buildDate>
                    <checksum>a32543207ddbf94165dfb2ef2c8e28e2</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaManagerWSClient" name="Media Manager WS Client">
                <pluginVersion>1.0.7.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-04 14:47:21-0500</buildDate>
                    <checksum>ba30fddfdeb81302c3ffd95cb767d5e8</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.SMILUtilities" name="SMILUtilities">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-30 11:25:03-0400</buildDate>
                    <checksum>1c224b9e3c0b7402b832559b1a9350c3</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.SMPTE291Demuxer" name="SMPTE291Demuxer">
                <pluginVersion>1.0.22.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-15 13:27:15-0400</buildDate>
                    <checksum>6309f999c8bdb5e7026ec5a95ac30262</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.StreamSynchronizers" name="StreamSynchronizers">
                <pluginVersion>1.0.39.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-11 17:20:33-0400</buildDate>
                    <checksum>ba1e268ef0281f32aa9e7acd337e6d79</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.TTMLEncoder" name="TTMLEncoder">
                <pluginVersion>1.0.52.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-28 13:36:20-0400</buildDate>
                    <checksum>f2d49437a86e8517476691f38a0b9555</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.TimecodeEncoder" name="TimecodeEncoder">
                <pluginVersion>1.0.27.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-10 10:25:39-0400</buildDate>
                    <checksum>77809288cc3ae400a380e49ee7defbe0</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.TimedTextSelector" name="TimedTextSelector">
                <pluginVersion>1.0.13.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-05 06:57:41-0400</buildDate>
                    <checksum>e85e1f41dd602e31a7bf14d697034ef4</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoBorder" name="VideoBorder">
                <pluginVersion>1.0.36.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-11 14:02:21-0400</buildDate>
                    <checksum>6df5a30270db376ec617343c1b631832</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoDeinterlacers" name="VideoDeinterlacers">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-23 14:22:16-0400</buildDate>
                    <checksum>b81fd23b4334dbdba1b981c9577f82e5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter" name="VideoFormatConverter">
                <pluginVersion>1.0.50.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-05 06:53:06-0400</buildDate>
                    <checksum>a0f5e57fc1d02630661efe8fb11a290e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFormatUtilities" name="VideoFormatUtilities">
                <pluginVersion>1.0.69.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-29 10:35:46-0400</buildDate>
                    <checksum>8da2a92cea3212a36719ec3b7bdc5bb0</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoProcessor" name="VideoProcessor">
                <pluginVersion>1.0.16.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-05 06:52:51-0400</buildDate>
                    <checksum>0aa75f5eed320aec0aba94dd57847f3a</checksum>
                </buildInfo>
            </plugin>
        </plugins>
        <pluginIdentifiers>
            <plugin name="AAC Source Controller" buildDate="2014-04-14 15:07:54-0400" pluginId="ca.digitalrapids.AACSourceController" pluginVersion="1.0.24.0" platform="Windows" checksum="b222e9b8c8a949a47a1e02bb9a195755"/>
            <plugin name="AC3 Source Controller" buildDate="2014-06-05 15:15:06-0400" pluginId="ca.digitalrapids.AC3SourceController" pluginVersion="1.0.28.0" platform="Windows" checksum="62e71f1beeef7aece22de090ba387568"/>
            <plugin name="AES3AudioProcessor" buildDate="2014-08-21 10:18:34-0400" pluginId="ca.digitalrapids.AES3AudioProcessor" pluginVersion="1.0.17.0" platform="Windows" checksum="ecd1dbfbdf4b5cc66ebfe984c4710466"/>
            <plugin name="AES3SourceController" buildDate="2014-08-18 14:51:13-0400" pluginId="ca.digitalrapids.AES3SourceController" pluginVersion="1.0.22.0" platform="Windows" checksum="b3f12b371c965d668d8319a9119403b7"/>
            <plugin name="AVC Source Controller" buildDate="2014-06-20 16:41:09-0400" pluginId="ca.digitalrapids.AVCSourceController" pluginVersion="1.0.41.0" platform="Windows" checksum="f4141437f06529b2a3d69216ed1a5a23"/>
            <plugin name="Assets" buildDate="2013-10-18 16:43:49-0400" pluginId="ca.digitalrapids.Assets" pluginVersion="1.0.4.0" platform="Generic" checksum="255263cf15e1f4f2eedd6b3818774c5e"/>
            <plugin name="AudioFormatConverter" buildDate="2014-06-05 06:46:46-0400" pluginId="ca.digitalrapids.AudioFormatConverter" pluginVersion="1.0.15.0" platform="Generic" checksum="9600f0c46dbee32ddb35457459e78965"/>
            <plugin name="AudioFormatUtilities" buildDate="2014-08-29 15:37:46-0400" pluginId="ca.digitalrapids.AudioFormatUtilities" pluginVersion="1.0.66.0" platform="Windows" checksum="9705e4a64a38cca30806aaf4971e1043"/>
            <plugin name="CC608Decoder" buildDate="2014-06-05 06:48:28-0400" pluginId="ca.digitalrapids.CC608Decoder" pluginVersion="1.0.30.0" platform="Windows" checksum="6a17c59dfc34f9ef5cba3bb1f6c964f1"/>
            <plugin name="ClosedCaptionsUtilities" buildDate="2014-06-04 22:02:16-0400" pluginId="ca.digitalrapids.ClosedCaptionsUtilities" pluginVersion="1.0.36.0" platform="Windows" checksum="fe6129d46cd0265e7aaa539965c72caf"/>
            <plugin name="CommonAAC" buildDate="2013-10-17 15:15:13-0400" pluginId="ca.digitalrapids.CommonAAC" pluginVersion="1.0.6.0" platform="Generic" checksum="ac4d12dbec71f08576aebabfd714e796"/>
            <plugin name="CommonAC3" buildDate="2013-10-17 15:19:06-0400" pluginId="ca.digitalrapids.CommonAC3" pluginVersion="1.1.9.0" platform="Generic" checksum="5d89d2d67175f9f780957eb87758fdb9"/>
            <plugin name="CommonAES3" buildDate="2014-08-07 16:36:41-0400" pluginId="ca.digitalrapids.CommonAES3" pluginVersion="1.1.3.0" platform="Generic" checksum="a2e5b5f10dccddeebbf46747cfc4b273"/>
            <plugin name="CommonAVC" buildDate="2014-07-25 08:30:23-0400" pluginId="ca.digitalrapids.CommonAVC" pluginVersion="1.0.19.0" platform="Windows" checksum="fcb0f15f2ff405925f2482ad19995b63"/>
            <plugin name="CommonComponents" buildDate="2014-08-01 16:18:33-0400" pluginId="ca.digitalrapids.CommonComponents" pluginVersion="1.1.42.0" platform="Generic" checksum="4f3a3a8519a193083a3538ea3b2f3419"/>
            <plugin name="CommonDTS" buildDate="2013-10-18 15:01:07-0400" pluginId="ca.digitalrapids.CommonDTS" pluginVersion="1.0.4.0" platform="Generic" checksum="61f3136c4769493132791ab20eec14d2"/>
            <plugin name="CommonDV" buildDate="2013-10-18 15:06:45-0400" pluginId="ca.digitalrapids.CommonDV" pluginVersion="1.0.2.0" platform="Generic" checksum="0ef596aedc812db9e887d54b06211d7b"/>
            <plugin name="CommonDolbyE" buildDate="2013-10-18 14:48:14-0400" pluginId="ca.digitalrapids.CommonDolbyE" pluginVersion="1.0.3.0" platform="Generic" checksum="8d9db933132fff45f77d1cca159caca3"/>
            <plugin name="CommonEAC3" buildDate="2013-10-18 15:10:24-0400" pluginId="ca.digitalrapids.CommonEAC3" pluginVersion="1.0.6.0" platform="Generic" checksum="4c4b4e50362a99bd36be9cb33d8a90ea"/>
            <plugin name="CommonFont" buildDate="2014-02-21 19:27:37-0500" pluginId="ca.digitalrapids.CommonFont" pluginVersion="1.0.1.0" platform="Generic" checksum="385df375fa90cde4af26294936172be7"/>
            <plugin name="CommonImageFormats" buildDate="2013-11-27 18:24:43-0500" pluginId="ca.digitalrapids.CommonImageFormats" pluginVersion="1.0.20.0" platform="Generic" checksum="0a750fef4f8d6198f52719540c553974"/>
            <plugin name="CommonIntelIPP" buildDate="2013-10-18 15:39:11-0400" pluginId="ca.digitalrapids.CommonIntelIPP" pluginVersion="1.0.11.0" platform="Windows" checksum="e6f3740379a0221f52a423b40710dc08"/>
            <plugin name="CommonJ2KVideo" buildDate="2013-10-21 08:10:17-0400" pluginId="ca.digitalrapids.CommonJ2KVideo" pluginVersion="1.0.3.0" platform="Generic" checksum="bdf83e122eb86d00a3aeb8e42ff70fa2"/>
            <plugin name="CommonLanguage" buildDate="2014-06-04 21:58:14-0400" pluginId="ca.digitalrapids.CommonLanguage" pluginVersion="1.0.16.0" platform="Windows" checksum="d7c19cf7c0d9bc0762ad70b66dee7eb5"/>
            <plugin name="CommonMPEG" buildDate="2013-10-21 13:13:48-0400" pluginId="ca.digitalrapids.CommonMPEG" pluginVersion="1.0.4.0" platform="Generic" checksum="a934a85eb4d0c86a757d80a792ae032c"/>
            <plugin name="CommonMPEG1" buildDate="2013-10-21 13:16:50-0400" pluginId="ca.digitalrapids.CommonMPEG1" pluginVersion="1.0.4.0" platform="Generic" checksum="205521ae64fe69e65ef74070b07d321e"/>
            <plugin name="CommonMPEG2" buildDate="2014-08-06 13:58:27-0400" pluginId="ca.digitalrapids.CommonMPEG2" pluginVersion="1.0.14.0" platform="Generic" checksum="29f0e81dcbd0d42b41e514d90009b6bd"/>
            <plugin name="CommonMPEG4" buildDate="2013-10-21 13:26:02-0400" pluginId="ca.digitalrapids.CommonMPEG4" pluginVersion="1.0.11.0" platform="Generic" checksum="ece896a89240b90c772bf89fb8179658"/>
            <plugin name="CommonMXF" buildDate="2013-10-21 13:28:57-0400" pluginId="ca.digitalrapids.CommonMXF" pluginVersion="1.0.3.0" platform="Generic" checksum="7cb12460dc147b18de8b15254e47df2a"/>
            <plugin name="CommonMedia" buildDate="2014-08-07 16:36:29-0400" pluginId="ca.digitalrapids.CommonMedia" pluginVersion="1.0.57.0" platform="Windows" checksum="eadf65d8ecd33d69316395a7edbd8f9e"/>
            <plugin name="CommonMediaEncryption" buildDate="2014-06-27 17:24:40-0400" pluginId="ca.digitalrapids.CommonMediaEncryption" pluginVersion="1.0.7.0" platform="Generic" checksum="e8137cd8314ceb3134bd2569645d0cd2"/>
            <plugin name="CommonMetadata" buildDate="2013-10-21 12:54:21-0400" pluginId="ca.digitalrapids.CommonMetadata" pluginVersion="1.0.5.0" platform="Generic" checksum="69fc09d4a2979ccfd562a4ef693a60d7"/>
            <plugin name="CommonPlayReadyEncryption" buildDate="2013-10-21 13:59:20-0400" pluginId="ca.digitalrapids.CommonPlayReadyEncryption" pluginVersion="1.0.5.0" platform="Generic" checksum="f612be0829038b2d63b80d948dff3ee8"/>
            <plugin name="CommonQuickTime" buildDate="2013-10-21 15:35:15-0400" pluginId="ca.digitalrapids.CommonQuickTime" pluginVersion="1.0.3.0" platform="Generic" checksum="2f8c5ead8bfb9e7f614f6c9ae9684166"/>
            <plugin name="CommonStereoVideo" buildDate="2013-10-21 15:48:17-0400" pluginId="ca.digitalrapids.CommonStereoVideo" pluginVersion="1.0.2.0" platform="Generic" checksum="f1f78354a42e6e4a6a3aa17fa3be846f"/>
            <plugin name="CommonSubtitles" buildDate="2014-04-21 17:41:29-0400" pluginId="ca.digitalrapids.CommonSubtitles" pluginVersion="1.0.10.0" platform="Generic" checksum="5349794c466bcbd4bb5ef0b7940fe0c2"/>
            <plugin name="CommonTimecode" buildDate="2013-11-06 15:04:52-0500" pluginId="ca.digitalrapids.CommonTimecode" pluginVersion="1.0.11.0" platform="Generic" checksum="0cdd77e7773cb540efb3cfc8396ca377"/>
            <plugin name="CommonUltraviolet" buildDate="2014-03-25 11:15:24-0400" pluginId="ca.digitalrapids.CommonUltraviolet" pluginVersion="1.0.3.0" platform="Generic" checksum="0fbaf01cd371c81f4fcb7cf66f41f6d9"/>
            <plugin name="CommonVC3" buildDate="2013-11-06 15:27:08-0500" pluginId="ca.digitalrapids.CommonVC3" pluginVersion="1.0.3.0" platform="Generic" checksum="ada6c958950fadf2780b72caac6bd90c"/>
            <plugin name="CommonVideoSystem" buildDate="2014-04-01 13:53:08-0400" pluginId="ca.digitalrapids.CommonVideoSystem" pluginVersion="1.0.52.0" platform="Windows" checksum="9854595f8a2afa2d07eb5cf1092414d4"/>
            <plugin name="CommonWAVE" buildDate="2013-10-21 17:02:27-0400" pluginId="ca.digitalrapids.CommonWAVE" pluginVersion="1.0.3.0" platform="Generic" checksum="df370b1fb4c0a1c420d8c1385c428223"/>
            <plugin name="DRAVCEncoder" buildDate="2014-07-25 08:32:06-0400" pluginId="ca.digitalrapids.DRAVCEncoder" pluginVersion="1.0.82.0" platform="Windows" checksum="d37092eb8d015396a7082a4117756f91"/>
            <plugin name="DRColorspaceConverter" buildDate="2014-05-23 09:38:41-0400" pluginId="ca.digitalrapids.DRColorspaceConverter" pluginVersion="1.2.4.0" platform="Windows" checksum="8661380f60443341bff9477a31fe264f"/>
            <plugin name="DRDeinterlacer" buildDate="2014-08-06 15:02:19-0400" pluginId="ca.digitalrapids.DRDeinterlacer" pluginVersion="1.4.11.0" platform="Windows" checksum="5b7d25a750a9896f4e97321b79dd215e"/>
            <plugin name="DRMediaProcessing" buildDate="2014-07-15 16:26:44-0400" pluginId="ca.digitalrapids.DRMediaProcessing" pluginVersion="2.5.3.0" platform="Windows" checksum="a0fe144ff3d06f6413126c08240bd42f"/>
            <plugin name="DRProgressiveToInterlaced" buildDate="2014-06-06 15:16:49-0400" pluginId="ca.digitalrapids.DRProgressiveToInterlaced" pluginVersion="1.1.11.0" platform="Windows" checksum="3b980be7bfcfb666ec214c65afc42b5f"/>
            <plugin name="DRScaler" buildDate="2014-08-06 15:40:19-0400" pluginId="ca.digitalrapids.DRScaler" pluginVersion="1.2.11.0" platform="Windows" checksum="a11ff459fd62b17711259e799f8c8e3d"/>
            <plugin name="DTS Source Controller" buildDate="2013-11-01 15:22:20-0400" pluginId="ca.digitalrapids.DTSSourceController" pluginVersion="1.0.12.0" platform="Windows" checksum="374aaf0ece6f5fecefc8c73de24a6f3e"/>
            <plugin name="DirectShowFileSource" buildDate="2013-11-20 21:17:46-0500" pluginId="ca.digitalrapids.DirectShowFileSource" pluginVersion="1.0.19.0" platform="Windows" checksum="e35fae50cdcf706cb794dd1bd14b35cf"/>
            <plugin name="Dolby E Source Controller" buildDate="2014-08-07 16:37:41-0400" pluginId="ca.digitalrapids.DolbyESourceController" pluginVersion="1.0.6.0" platform="Windows" checksum="857e18a3b26edee7a84f8eae5f741178"/>
            <plugin name="DolbyPulseEncoder" buildDate="2014-08-07 16:42:48-0400" pluginId="ca.digitalrapids.DolbyPulseEncoder" pluginVersion="1.0.30.0" platform="Windows" checksum="e15cba882effe4b96d06111bf86ef037"/>
            <plugin name="EAC3 Source Controller" buildDate="2013-11-01 15:50:54-0400" pluginId="ca.digitalrapids.EAC3SourceController" pluginVersion="1.0.12.0" platform="Windows" checksum="c237507b50e8d6842ffb8e24af37a314"/>
            <plugin name="EIACaptionsRetimer" buildDate="2014-06-04 22:04:06-0400" pluginId="ca.digitalrapids.EIACaptionsRetimer" pluginVersion="1.0.15.0" platform="Windows" checksum="a8b1b125c02afb734b2bee78ac3f04a4"/>
            <plugin name="HTTP Utilities" buildDate="2014-06-05 06:46:22-0400" pluginId="ca.digitalrapids.HttpUtilities" pluginVersion="1.2.22.0" platform="Generic" checksum="dc54776f1c4464b6228801ad4169d9c5"/>
            <plugin name="KayakCore" buildDate="2014-11-18 15:58:18-0500" pluginId="ca.digitalrapids.KayakCore" pluginVersion="1.3.7.1" platform="Windows" checksum="a6fcff68bae2ff9865525c820e4a8937"/>
            <plugin name="KayakDesigner" buildDate="2014-11-18 16:54:18-0500" pluginId="ca.digitalrapids.KayakDesigner" pluginVersion="1.3.7.1" platform="Generic" checksum="38b1a0666781ebd5bd12a51f94098a84"/>
            <plugin name="MPEG2AudioSourceController" buildDate="2014-06-06 11:19:39-0400" pluginId="ca.digitalrapids.MPEG2AudioSourceController" pluginVersion="1.0.27.0" platform="Windows" checksum="113876451453065f6475052338cbd1bc"/>
            <plugin name="MPEG2UDDemuxer" buildDate="2014-05-26 15:16:23-0400" pluginId="ca.digitalrapids.MPEG2UDDemuxer" pluginVersion="1.1.24.0" platform="Windows" checksum="5ec641a119f018ac0572e7d8143f8562"/>
            <plugin name="MPEG2UDMuxer" buildDate="2014-06-23 15:54:46-0400" pluginId="ca.digitalrapids.MPEG2UDMuxer" pluginVersion="1.0.26.0" platform="Windows" checksum="ffc0f3083cd69dc13b4923dc343b9208"/>
            <plugin name="MPEG2VideoSourceController" buildDate="2014-07-31 09:14:36-0400" pluginId="ca.digitalrapids.MPEG2VideoSourceController" pluginVersion="1.0.24.0" platform="Windows" checksum="5020a7167ae47cb1c1c648bb53f2d8c8"/>
            <plugin name="MPEG4Muxer" buildDate="2014-07-07 13:56:50-0400" pluginId="ca.digitalrapids.MPEG4Muxer" pluginVersion="1.1.58.0" platform="Windows" checksum="1871d8f170ad6194b499611175662bab"/>
            <plugin name="MPEGVideoDecoder" buildDate="2014-07-29 09:55:42-0400" pluginId="ca.digitalrapids.MPEGVideoDecoder" pluginVersion="1.0.18.0" platform="Windows" checksum="2f007147892731f873236a49c704358f"/>
            <plugin name="MXFDemuxer" buildDate="2014-08-26 13:20:07-0400" pluginId="ca.digitalrapids.MXFDemuxer" pluginVersion="1.0.46.0" platform="Windows" checksum="a77dd94af3f4525030b7e8a0005b2be5"/>
            <plugin name="MediaInspection" buildDate="2014-08-11 12:48:41-0400" pluginId="ca.digitalrapids.MediaInspection" pluginVersion="1.0.42.0" platform="Generic" checksum="ff66be769001ae9abfc6a170744e7a67"/>
            <plugin name="Media Manager" buildDate="2014-06-12 15:34:14-0400" pluginId="ca.digitalrapids.MediaManager" pluginVersion="1.0.49.0" platform="Generic" checksum="a32543207ddbf94165dfb2ef2c8e28e2"/>
            <plugin name="Media Manager WS Client" buildDate="2013-11-04 14:47:21-0500" pluginId="ca.digitalrapids.MediaManagerWSClient" pluginVersion="1.0.7.0" platform="Generic" checksum="ba30fddfdeb81302c3ffd95cb767d5e8"/>
            <plugin name="SMILUtilities" buildDate="2013-10-30 11:25:03-0400" pluginId="ca.digitalrapids.SMILUtilities" pluginVersion="1.0.11.0" platform="Generic" checksum="1c224b9e3c0b7402b832559b1a9350c3"/>
            <plugin name="SMPTE291Demuxer" buildDate="2014-07-15 13:27:15-0400" pluginId="ca.digitalrapids.SMPTE291Demuxer" pluginVersion="1.0.22.0" platform="Windows" checksum="6309f999c8bdb5e7026ec5a95ac30262"/>
            <plugin name="StreamSynchronizers" buildDate="2014-08-11 17:20:33-0400" pluginId="ca.digitalrapids.StreamSynchronizers" pluginVersion="1.0.39.0" platform="Windows" checksum="ba1e268ef0281f32aa9e7acd337e6d79"/>
            <plugin name="TTMLEncoder" buildDate="2014-07-28 13:36:20-0400" pluginId="ca.digitalrapids.TTMLEncoder" pluginVersion="1.0.52.0" platform="Generic" checksum="f2d49437a86e8517476691f38a0b9555"/>
            <plugin name="TimecodeEncoder" buildDate="2014-07-10 10:25:39-0400" pluginId="ca.digitalrapids.TimecodeEncoder" pluginVersion="1.0.27.0" platform="Windows" checksum="77809288cc3ae400a380e49ee7defbe0"/>
            <plugin name="TimedTextSelector" buildDate="2014-06-05 06:57:41-0400" pluginId="ca.digitalrapids.TimedTextSelector" pluginVersion="1.0.13.0" platform="Generic" checksum="e85e1f41dd602e31a7bf14d697034ef4"/>
            <plugin name="VideoBorder" buildDate="2014-08-11 14:02:21-0400" pluginId="ca.digitalrapids.VideoBorder" pluginVersion="1.0.36.0" platform="Windows" checksum="6df5a30270db376ec617343c1b631832"/>
            <plugin name="VideoDeinterlacers" buildDate="2013-10-23 14:22:16-0400" pluginId="ca.digitalrapids.VideoDeinterlacers" pluginVersion="1.0.11.0" platform="Windows" checksum="b81fd23b4334dbdba1b981c9577f82e5"/>
            <plugin name="VideoFormatConverter" buildDate="2014-06-05 06:53:06-0400" pluginId="ca.digitalrapids.VideoFormatConverter" pluginVersion="1.0.50.0" platform="Generic" checksum="a0f5e57fc1d02630661efe8fb11a290e"/>
            <plugin name="VideoFormatUtilities" buildDate="2014-07-29 10:35:46-0400" pluginId="ca.digitalrapids.VideoFormatUtilities" pluginVersion="1.0.69.0" platform="Windows" checksum="8da2a92cea3212a36719ec3b7bdc5bb0"/>
            <plugin name="VideoProcessor" buildDate="2014-06-05 06:52:51-0400" pluginId="ca.digitalrapids.VideoProcessor" pluginVersion="1.0.16.0" platform="Windows" checksum="0aa75f5eed320aec0aba94dd57847f3a"/>
        </pluginIdentifiers>
    </authoringInfo>
    <dependencyInfo>
        <plugins>
            <plugin pluginId="ca.digitalrapids.AACSourceController" name="AAC Source Controller"/>
            <plugin pluginId="ca.digitalrapids.AC3SourceController" name="AC3 Source Controller"/>
            <plugin pluginId="ca.digitalrapids.AES3AudioProcessor" name="AES3AudioProcessor"/>
            <plugin pluginId="ca.digitalrapids.AES3SourceController" name="AES3SourceController"/>
            <plugin pluginId="ca.digitalrapids.AVCSourceController" name="AVC Source Controller"/>
            <plugin pluginId="ca.digitalrapids.Assets" name="Assets"/>
            <plugin pluginId="ca.digitalrapids.AudioFormatConverter" name="AudioFormatConverter"/>
            <plugin pluginId="ca.digitalrapids.AudioFormatUtilities" name="AudioFormatUtilities"/>
            <plugin pluginId="ca.digitalrapids.CC608Decoder" name="CC608Decoder"/>
            <plugin pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="ClosedCaptionsUtilities"/>
            <plugin pluginId="ca.digitalrapids.CommonAAC" name="CommonAAC"/>
            <plugin pluginId="ca.digitalrapids.CommonAC3" name="CommonAC3"/>
            <plugin pluginId="ca.digitalrapids.CommonAES3" name="CommonAES3"/>
            <plugin pluginId="ca.digitalrapids.CommonAVC" name="CommonAVC"/>
            <plugin pluginId="ca.digitalrapids.CommonComponents" name="CommonComponents"/>
            <plugin pluginId="ca.digitalrapids.CommonDTS" name="CommonDTS"/>
            <plugin pluginId="ca.digitalrapids.CommonDV" name="CommonDV"/>
            <plugin pluginId="ca.digitalrapids.CommonDolbyE" name="CommonDolbyE"/>
            <plugin pluginId="ca.digitalrapids.CommonEAC3" name="CommonEAC3"/>
            <plugin pluginId="ca.digitalrapids.CommonFont" name="CommonFont"/>
            <plugin pluginId="ca.digitalrapids.CommonImageFormats" name="CommonImageFormats"/>
            <plugin pluginId="ca.digitalrapids.CommonIntelIPP" name="CommonIntelIPP"/>
            <plugin pluginId="ca.digitalrapids.CommonJ2KVideo" name="CommonJ2KVideo"/>
            <plugin pluginId="ca.digitalrapids.CommonLanguage" name="CommonLanguage"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG" name="CommonMPEG"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG1" name="CommonMPEG1"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG2" name="CommonMPEG2"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG4" name="CommonMPEG4"/>
            <plugin pluginId="ca.digitalrapids.CommonMXF" name="CommonMXF"/>
            <plugin pluginId="ca.digitalrapids.CommonMedia" name="CommonMedia"/>
            <plugin pluginId="ca.digitalrapids.CommonMediaEncryption" name="CommonMediaEncryption"/>
            <plugin pluginId="ca.digitalrapids.CommonMetadata" name="CommonMetadata"/>
            <plugin pluginId="ca.digitalrapids.CommonPlayReadyEncryption" name="CommonPlayReadyEncryption"/>
            <plugin pluginId="ca.digitalrapids.CommonQuickTime" name="CommonQuickTime"/>
            <plugin pluginId="ca.digitalrapids.CommonStereoVideo" name="CommonStereoVideo"/>
            <plugin pluginId="ca.digitalrapids.CommonSubtitles" name="CommonSubtitles"/>
            <plugin pluginId="ca.digitalrapids.CommonTimecode" name="CommonTimecode"/>
            <plugin pluginId="ca.digitalrapids.CommonUltraviolet" name="CommonUltraviolet"/>
            <plugin pluginId="ca.digitalrapids.CommonVC3" name="CommonVC3"/>
            <plugin pluginId="ca.digitalrapids.CommonVideoSystem" name="CommonVideoSystem"/>
            <plugin pluginId="ca.digitalrapids.CommonWAVE" name="CommonWAVE"/>
            <plugin pluginId="ca.digitalrapids.DRAVCEncoder" name="DRAVCEncoder"/>
            <plugin pluginId="ca.digitalrapids.DRColorspaceConverter" name="DRColorspaceConverter"/>
            <plugin pluginId="ca.digitalrapids.DRDeinterlacer" name="DRDeinterlacer"/>
            <plugin pluginId="ca.digitalrapids.DRMediaProcessing" name="DRMediaProcessing"/>
            <plugin pluginId="ca.digitalrapids.DRProgressiveToInterlaced" name="DRProgressiveToInterlaced"/>
            <plugin pluginId="ca.digitalrapids.DRScaler" name="DRScaler"/>
            <plugin pluginId="ca.digitalrapids.DTSSourceController" name="DTS Source Controller"/>
            <plugin pluginId="ca.digitalrapids.DirectShowFileSource" name="DirectShowFileSource"/>
            <plugin pluginId="ca.digitalrapids.DolbyESourceController" name="Dolby E Source Controller"/>
            <plugin pluginId="ca.digitalrapids.DolbyPulseEncoder" name="DolbyPulseEncoder"/>
            <plugin pluginId="ca.digitalrapids.EAC3SourceController" name="EAC3 Source Controller"/>
            <plugin pluginId="ca.digitalrapids.EIACaptionsRetimer" name="EIACaptionsRetimer"/>
            <plugin pluginId="ca.digitalrapids.HttpUtilities" name="HTTP Utilities"/>
            <plugin pluginId="ca.digitalrapids.KayakCore" name="KayakCore"/>
            <plugin pluginId="ca.digitalrapids.KayakDesigner" name="KayakDesigner"/>
            <plugin pluginId="ca.digitalrapids.MPEG2AudioSourceController" name="MPEG2AudioSourceController"/>
            <plugin pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2UDDemuxer"/>
            <plugin pluginId="ca.digitalrapids.MPEG2UDMuxer" name="MPEG2UDMuxer"/>
            <plugin pluginId="ca.digitalrapids.MPEG2VideoSourceController" name="MPEG2VideoSourceController"/>
            <plugin pluginId="ca.digitalrapids.MPEG4Muxer" name="MPEG4Muxer"/>
            <plugin pluginId="ca.digitalrapids.MPEGVideoDecoder" name="MPEGVideoDecoder"/>
            <plugin pluginId="ca.digitalrapids.MXFDemuxer" name="MXFDemuxer"/>
            <plugin pluginId="ca.digitalrapids.MediaInspection" name="MediaInspection"/>
            <plugin pluginId="ca.digitalrapids.MediaManager" name="Media Manager"/>
            <plugin pluginId="ca.digitalrapids.MediaManagerWSClient" name="Media Manager WS Client"/>
            <plugin pluginId="ca.digitalrapids.SMILUtilities" name="SMILUtilities"/>
            <plugin pluginId="ca.digitalrapids.SMPTE291Demuxer" name="SMPTE291Demuxer"/>
            <plugin pluginId="ca.digitalrapids.StreamSynchronizers" name="StreamSynchronizers"/>
            <plugin pluginId="ca.digitalrapids.TTMLEncoder" name="TTMLEncoder"/>
            <plugin pluginId="ca.digitalrapids.TimecodeEncoder" name="TimecodeEncoder"/>
            <plugin pluginId="ca.digitalrapids.TimedTextSelector" name="TimedTextSelector"/>
            <plugin pluginId="ca.digitalrapids.VideoBorder" name="VideoBorder"/>
            <plugin pluginId="ca.digitalrapids.VideoDeinterlacers" name="VideoDeinterlacers"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter" name="VideoFormatConverter"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatUtilities" name="VideoFormatUtilities"/>
            <plugin pluginId="ca.digitalrapids.VideoProcessor" name="VideoProcessor"/>
        </plugins>
        <components>
            <component pluginId="ca.digitalrapids.AACSourceController" name="AAC ADTS to Raw Converter" guid="eed0ba59-346f-47b0-ba9a-2ea14be6fa53"/>
            <component pluginId="ca.digitalrapids.AACSourceController" name="AAC Controller" guid="784ee2cc-8a15-41c9-b84b-1a79ced4a646"/>
            <component pluginId="ca.digitalrapids.DolbyPulseEncoder" name="AAC Encoder - Dolby Pulse" displayName="AAC Encoder (Dolby)" guid="D0933A55-4818-4ADC-9301-8BE7687AC9E3"/>
            <component pluginId="ca.digitalrapids.DolbyPulseEncoder" name="Atomic AAC Encoder - Dolby Pulse" displayName="AAC Encoder Core (Dolby)" guid="8916cfea-3397-4310-b5bb-402e27fb0baf"/>
            <component pluginId="ca.digitalrapids.AES3SourceController" name="AES3 Controller" guid="27e78f33-8cf3-4b32-bb7c-03009984567f"/>
            <component pluginId="ca.digitalrapids.AES3SourceController" name="AES3 Media Inspector" guid="c1718874-b04c-48a6-a1c6-120bfb4a928a"/>
            <component pluginId="ca.digitalrapids.SMPTE291Demuxer" name="SMPTE291 Demultiplexer" displayName="Ancillary Data Demultiplexer" guid="DF2C29B3-52A5-4786-8C1D-3AE207D252D4"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Pixel Aspect Ratio Updater" displayName="Aspect Ratio Updater" guid="d75a869d-be42-43e9-b74d-b7d04adf1ae5"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Audio Container Data Type Merger" guid="3AED3909-CEC8-4413-BF58-33FA08514D0C"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Audio Data Type Updater" guid="9D095BEC-5A2C-445e-9AF9-A17313693263"/>
            <component pluginId="ca.digitalrapids.AudioFormatConverter" name="Audio Format Converter" guid="F2A4515C-ABD5-49f9-B0D5-DB462E4BB674"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Audio Sample Rate Updater" guid="1D20E701-CE2C-4155-9C95-0F509EFFF4AF"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Audio Stream Interleaver" guid="D166D48B-FA26-44ca-8F2D-62B20D892659"/>
            <component pluginId="ca.digitalrapids.AVCSourceController" name="AVC Controller" guid="6ae5cf5f-3a25-4f61-8e3c-16c33b474d4c"/>
            <component pluginId="ca.digitalrapids.AVCSourceController" name="AVC Part10 to Part15 Converter" guid="b2eC0208-f841-4272-8a16-4b88e80d86a5"/>
            <component pluginId="ca.digitalrapids.DRAVCEncoder" name="Advanced AVC Encoder" displayName="AVC Video Encoder" guid="A3597472-D51E-44d9-9F0A-395744A83FA3"/>
            <component pluginId="ca.digitalrapids.DRAVCEncoder" name="AVC Encoder" displayName="AVC Video Encoder Core" guid="16c55dc4-7cd8-4d25-bfec-1cc4aebad739"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Bit Depth Converter" guid="7DF81BC0-6DFD-44fd-BDAA-2E568F65CFF6"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="BranchMerger" displayName="Branch Merger" guid="5442b6d8-5672-430f-91ce-5b047e84bd7f"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Channel Mapper" displayName="Channel Remapper" guid="771ACEB1-E611-4803-A356-21F221E3753D"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Color Space Converter - Intel" displayName="Color Space Converter - Intel" guid="2FDE07E0-7DBF-47e2-BC73-91F3B82D4392"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Container Data Type Merger" guid="b6eac4c1-3c04-4f8d-9654-96da605b9e90"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Data Type Merger" guid="4971c1a4-07ab-4c9a-93a6-947526a1005d"/>
            <component pluginId="ca.digitalrapids.DRDeinterlacer" name="DRDeinterlacer" displayName="Deinterlacer" guid="750D51F3-FC19-410f-89AB-B7F3E8CAFEDC"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Down Mix 5.1 to Stereo" guid="9B591F12-F688-4776-8F56-EC22E1DC367E"/>
            <component pluginId="ca.digitalrapids.EIACaptionsRetimer" name="EIA Captions Retimer" guid="8898EE2F-9122-4376-BA58-53F839725A5B"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="EIA Captions Null Packet Generator" displayName="EIA Captions Termination" guid="2D641C6D-980A-4748-B766-FD407B85795D"/>
            <component pluginId="ca.digitalrapids.CC608Decoder" name="Advanced 608 Closed Captions Decoder" displayName="EIA-608 Captions Decoder" guid="435A116E-392C-4300-B9A9-26FD56C95063"/>
            <component pluginId="ca.digitalrapids.CC608Decoder" name="608 Closed Captions Decoder" displayName="EIA-608 Captions Decoder Core" guid="5568d795-01c5-47fc-bb95-d15d84fc6c22"/>
            <component pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="EIA-608 Captions Selector" guid="0C2E5B8D-872A-4487-9D4D-07CECFD0F58C"/>
            <component pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="EIA-708 to 608 Converter" displayName="EIA-708 to 608 De-Embedder" guid="57CA8716-84CF-4C7F-B59F-DF34AFE2E73E"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="EndOfStreamNotification" displayName="End of Stream Notification" guid="285BF6A1-3FEA-4c2a-9D2D-4DB4B965C3EA"/>
            <component pluginId="ca.digitalrapids.CommonMedia" name="Endian Converter" guid="D076A34F-6E7D-46BD-875A-4C590B5538BF"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="File Output" guid="9b376163-de8d-4e09-8bed-353725b6b6d6"/>
            <component pluginId="ca.digitalrapids.MPEG4Muxer" name="Advanced ISO MPEG4 Multiplexer" displayName="ISO MPEG-4 Multiplexer" guid="E25468C3-A65C-4f1a-8172-E72CE4B63A70"/>
            <component pluginId="ca.digitalrapids.MPEG4Muxer" name="ISO MPEG4 Multiplexer" displayName="ISO MPEG-4 Multiplexer Core" guid="3CC47644-DC6D-4f2b-AB3B-580D305F47CC"/>
            <component pluginId="ca.digitalrapids.CommonLanguage" name="Language Code Updater" guid="563232cc-20ba-453f-8f69-43284cea7abc"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Logic Branch" guid="d3539080-4b93-48d5-a616-66de67cdd66a"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Logic Gate" guid="1824FAE2-9B42-4bb1-89D8-41104BC1B76C"/>
            <component pluginId="ca.digitalrapids.CommonMedia" name="Media Data Type Auto Updater" guid="9dc80c38-b4ff-4b3e-8324-2f29abeb461e"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media File Input" guid="7cec6ecd-a477-4834-bc6f-97e34aa58bb5"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media Inpection Data Type Merger" guid="A025A4BD-A59D-42e4-B00C-66F67BCB147C"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media Inspector" guid="3ada68f0-f492-4133-87e2-cdb55ae9f7fc"/>
            <component pluginId="ca.digitalrapids.MPEG2VideoSourceController" name="MPEG2 Video Controller" displayName="MPEG Video Controller" guid="232820c7-05c5-4938-b92e-19798db53a3c"/>
            <component pluginId="ca.digitalrapids.MPEGVideoDecoder" name="MPEG Video Decoder" displayName="MPEG Video Decoder" guid="63701505-b844-4f9e-8077-81065262388d"/>
            <component pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2 User Data Decoder" displayName="MPEG-2 User Data Decoder" guid="abc91f15-8728-463d-92c3-84a158b24248"/>
            <component pluginId="ca.digitalrapids.MPEG2UDMuxer" name="MPEG2UDMuxer" displayName="MPEG-2 User Data Encoder" guid="168499ec-1b75-471e-961b-306fdb111118"/>
            <component pluginId="ca.digitalrapids.MPEG2VideoSourceController" name="MPEG2 Video Media Inspector" displayName="MPEG-2 Video Media Inspector" guid="c920fef1-a139-477f-a630-bfc31ecf9d9c"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Multi-Program Audio Splitter" displayName="Multi-Program Audio Splitter" guid="6436a63f-1fa4-40e6-ba86-95138130d456"/>
            <component pluginId="ca.digitalrapids.MXFDemuxer" name="MXF Demultiplexer" guid="1A6B42BD-8131-41c3-8E51-361AB75A08B5"/>
            <component pluginId="ca.digitalrapids.MXFDemuxer" name="MXF Media Inspector" guid="C1261FE2-7506-4e7d-A6D7-5F576F723B2A"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Random Access File" guid="ef0bd6fd-7564-4efb-bb78-a184bce33a29"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Raw Audio Data Type Merger" guid="08D76F09-6818-4214-B4CF-0E7591556ADE"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Raw Audio Media Inspector" guid="F16BE80D-2AAB-4126-8820-1E05F64FB99D"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Raw Audio Processor" guid="9C0D7AA4-45A5-4561-B6EB-BCA2E0D4856F"/>
            <component pluginId="ca.digitalrapids.VideoProcessor" name="Reverse Field Dominance" guid="602FB90F-74C6-49cc-A6B3-DF9B920D6B69"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Sample Rate Converter" guid="0DAC861A-FDD8-4e0c-97BB-3341C4E46999"/>
            <component pluginId="ca.digitalrapids.SMILUtilities" name="SMIL Playlists Writer" guid="BC11DDED-63C0-4488-A7DB-309322189918"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Speaker Position Assigner" guid="AB851938-A3DA-4062-9F4A-FB8AF260D887"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="Stream Trimmer" guid="4EDCEFA6-93DE-463f-8C6B-543ED2CFCA77"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="Stream Truncator" guid="3B8118A9-72E6-42b7-910A-014D9E8C1575"/>
            <component pluginId="ca.digitalrapids.TimedTextSelector" name="Timed Text Selector" guid="8F552A81-B73E-46fA-9E14-145EC7DF3001"/>
            <component pluginId="ca.digitalrapids.MediaManager" name="Transcode Task Graph" displayName="Transcode Blueprint" guid="cc2f8f8a-85a3-4522-85a5-b0b26b12f4cd"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="Trimming Validator" guid="6D3E7814-6954-4e57-BF9C-AC843726A621"/>
            <component pluginId="ca.digitalrapids.TTMLEncoder" name="TTML Encoder" displayName="TTML Encoder" guid="1BED774D-D15A-444a-BD59-E113477BD6A6"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Video Data Type Updater" guid="D7576695-6BCB-410F-BB86-734E5F526924"/>
            <component pluginId="ca.digitalrapids.VideoFormatConverter" name="Video Format Converter" guid="AC185E0C-6839-4dae-A547-5E18DF5EA058"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Video Frame Rate Updater" guid="3EA4F9F3-9DBA-4E6A-A55E-53A8D0A2BAC2"/>
            <component pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="XDS Updater" guid="CA19BCE2-CCF5-408c-B0F6-94EC1AFB9A29"/>
            <component pluginId="ca.digitalrapids.KayakCore" name="Kayak Graph" displayName="Zenium Graph" guid="abc785f2-427e-4522-ba00-f3cb6acd1220"/>
            <component pluginId="ca.digitalrapids.KayakCore" name="Kayak OOP Graph" displayName="Zenium OOP Graph" guid="967a0d59-a62e-4c75-962c-4f65c180d45c"/>
        </components>
    </dependencyInfo>
</kayakDocument>
