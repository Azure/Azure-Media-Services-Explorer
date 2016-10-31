<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<kayakDocument version="1.2" xml:space="preserve">
    <components>
        <component>
            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
            <propertyDefinition displayName="Video 1 Bitrate" name="video1bitrate" group="Streaming Bitrates" dynamic="true">
                <propertyRedirect propertyPath="AVC Video Encoder 640x360 1200 kbps/AVC Encoder/rc.kbps"/>
            </propertyDefinition>
            <propertyDefinition displayName="Video 2 Bitrate" name="video2bitrate" group="Streaming Bitrates" dynamic="true">
                <propertyRedirect propertyPath="AVC Video Encoder 960x540 2500 kbps/AVC Encoder/rc.kbps"/>
            </propertyDefinition>
            <propertyDefinition displayName="Audio 1 Bitrate" name="audio1bitrate" group="Streaming Bitrates" dynamic="true">
                <propertyRedirect propertyPath="AAC Encoder (Dolby)/AAC Encoder/datarate"/>
            </propertyDefinition>
            <propertyDefinition displayName="Trimming Start Time" name="TrimmingStartTime" group="Trimming Info" dynamic="true">
                <propertyRedirect propertyPath="Stream Trimmer/StartTime"/>
            </propertyDefinition>
            <propertyDefinition displayName="Trimming End Time" name="TrimmingEndTime" group="Trimming Info" dynamic="true">
                <propertyRedirect propertyPath="Stream Trimmer/EndTime"/>
            </propertyDefinition>
            <property name="TrimmingEndTime">60</property>
            <property name="TrimmingStartTime">15</property>
            <property name="_graphDisplayContents" isNull="true"/>
            <property name="_graphMinDisplaySize" isNull="true"/>
            <property name="_logDebugInfoOnError" isNull="true"/>
            <property name="_timeBase_local" isNull="true"/>
            <property name="acquireChildLicenses" isNull="true"/>
            <property name="assetPieceNoMetadata" isNull="true"/>
            <property name="audio1bitrate">128000</property>
            <property name="clipListXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;clipList&gt;
    &lt;clip&gt;
        &lt;videoSource&gt;
            &lt;mediaFile&gt;
                &lt;file&gt;C:\Users\christoc\Desktop\Movie.mxf&lt;/file&gt;
            &lt;/mediaFile&gt;
        &lt;/videoSource&gt;
        &lt;audioSource&gt;
            &lt;mediaFile&gt;
                &lt;file&gt;C:\Users\christoc\Desktop\Movie.mxf&lt;/file&gt;
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
            <property name="inactiveTimeout">60</property>
            <property name="logsMaxEntries" isNull="true"/>
            <property name="monitorProgress">true</property>
            <property name="outputWriteDirectory">C:\Users\christoc\Desktop\Encoded Output</property>
            <property name="primarySourceFile">C:\Users\christoc\Desktop\Movie.mxf</property>
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
            <property name="video1bitrate">1200</property>
            <property name="video2bitrate">2500</property>
            <componentName>Transcode Blueprint</componentName>
            <componentDefinitionName>Transcode Task Graph</componentDefinitionName>
            <componentDefinitionGuid>cc2f8f8a-85a3-4522-85a5-b0b26b12f4cd</componentDefinitionGuid>
            <componentOwningPluginName>Media Manager</componentOwningPluginName>
            <componentOwningPluginId>ca.digitalrapids.MediaManager</componentOwningPluginId>
            <childComponents>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="Decode_captions_AFD">true</property>
                    <property name="EndTime" isNull="true"/>
                    <property name="StartTime" isNull="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">150.0,7.289996147155762</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
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
YS9EYXRhVHlwZTt4cAEAAAAAAAAAAXQAI0M6XFVzZXJzXGNocmlzdG9jXERlc2t0b3BcTW92aWUu
bXhmc3IALWNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLkJhc2VEYXRhVHlwZQAAAAAA
AAABAgADWgAHbXV0YWJsZUwAEmRhdGFUeXBlRGVmaW5pdGlvbnQARkxjYS9kaWdpdGFscmFwaWRz
L2theWFrL2RhdGF0eXBlcy9kZWZpbml0aW9uL21vZGVsL0RhdGFUeXBlRGVmaW5pdGlvbjtMAANt
YXB0AA9MamF2YS91dGlsL01hcDt4cAFwc3IAEWphdmEudXRpbC5IYXNoTWFwBQfawcMWYNEDAAJG
AApsb2FkRmFjdG9ySQAJdGhyZXNob2xkeHA/AAAAAAAAQHcIAAAAQAAAAAB4</property>
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
                    <pin name="UncompressedVideo" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedVideo" displayName="Uncompressed Video" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode" type="OUTPUT_IO">
                        <pinDefinition name="Timecode" displayName="Timecode (GOP Header)" type="OUTPUT_IO" dynamic="true"/>
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
                    <pin name="ActiveFormatDescriptionAndBarData" type="OUTPUT_IO">
                        <pinDefinition name="ActiveFormatDescriptionAndBarData" displayName="Active Format Description" type="OUTPUT_IO" dynamic="true"/>
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
                        <pinDefinition name="Timecode 3" displayName="Timecode (MXF Material Package)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 4" type="OUTPUT_IO">
                        <pinDefinition name="Timecode 4" displayName="Timecode (MXF System Track)" type="OUTPUT_IO" dynamic="true"/>
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
                    <pin name="UncompressedAudio 7" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio 7" displayName="Uncompressed Audio 7" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio 8" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio 8" displayName="Uncompressed Audio 8" type="OUTPUT_IO" dynamic="true"/>
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
                    <property name="_graphDisplayLocation">671.6024169921875,7.289996147155762</property>
                    <property name="_graphMinDisplaySize">1052.0,214.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
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
                    <property name="gen.level_idc">32</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">77</property>
                    <property name="gen.speed">8</property>
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
                    <property name="output_height">360</property>
                    <property name="output_width">640</property>
                    <property name="pluginOptions" isNull="true"/>
                    <property name="preset">yuyv</property>
                    <property name="pulldown_mode">2:3TFF</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">1200</property>
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
                    <property name="start_timecode">00:00:00:00/24</property>
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
                    <property name="vui.video_format">1</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <componentName>AVC Video Encoder 640x360 1200 kbps</componentName>
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
                    <property name="5_1_to_stereo" isNull="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">678.5023803710938,343.0900297164917</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
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
                    <property name="output_sample_rate" isNull="true"/>
                    <property name="phaseshift90">true</property>
                    <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                    <property name="removepce">false</property>
                    <property name="right_gain_db">0.0</property>
                    <property name="signallingmode">SBR_BC</property>
                    <property name="surround_gain_db">-3.0</property>
                    <property name="usePSv2">false</property>
                    <property name="use_metadata">true</property>
                    <componentName>AAC Encoder (Dolby)</componentName>
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
                    <property name="Duration">Depend on the longest source</property>
                    <property name="SpeakerPosition">true</property>
                    <property name="StartTime">Depend on the longest source</property>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">151.5500030517578,548.7899961471558</property>
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
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">593.9559936523438,548.7900266647339</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="channel1_speaker">L_LEFT</property>
                    <property name="channel2_speaker">R_RIGHT</property>
                    <property name="channel3_speaker"></property>
                    <property name="channel4_speaker"></property>
                    <property name="channel5_speaker"></property>
                    <property name="channel6_speaker"></property>
                    <property name="channel7_speaker"></property>
                    <property name="channel8_speaker"></property>
                    <property name="channel_group" isNull="true"/>
                    <property name="channel_position_preset">L_R</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="encoder_preset_filter"></property>
                    <property name="isChannelGroupSelector">false</property>
                    <property name="isSpeakerPositionAssigner">true</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_preset_mode">false</property>
                    <property name="override_mode">true</property>
                    <componentName>Speaker Position Assigner</componentName>
                    <componentDefinitionName>Speaker Position Assigner</componentDefinitionName>
                    <componentDefinitionGuid>AB851938-A3DA-4062-9F4A-FB8AF260D887</componentDefinitionGuid>
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
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1003.6024169921875,76.28999614715576</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
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
                    <property name="_graphDisplayLocation">1249.1024169921875,76.28999614715576</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="ROOT_video1bitrate" path="../../video1bitrate"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_video1bitrate}kbps.MP4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_video1bitrate}kbps.MP4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 1</componentName>
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
                    <property name="_graphDisplayLocation">674.8524169921875,170.3899908065796</property>
                    <property name="_graphMinDisplaySize">1052.0,214.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
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
                    <property name="gen.level_idc">32</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">77</property>
                    <property name="gen.speed">8</property>
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
                    <property name="output_height">540</property>
                    <property name="output_width">960</property>
                    <property name="pluginOptions" isNull="true"/>
                    <property name="preset">yuyv</property>
                    <property name="pulldown_mode">2:3TFF</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">2500</property>
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
                    <property name="start_timecode">00:00:00:00/24</property>
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
                    <property name="vui.video_format">1</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <componentName>AVC Video Encoder 960x540 2500 kbps</componentName>
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
                    <property name="_graphDisplayLocation">1004.5524291992188,207.1900053024292</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>ISO MPEG-4 Multiplexer 2</componentName>
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
                    <property name="_graphDisplayLocation">1248.9024047851562,207.1900053024292</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="ROOT_video2bitrate" path="../../video2bitrate"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_video2bitrate}kbps.MP4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_video2bitrate}kbps.MP4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 2</componentName>
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
                    <property name="_graphDisplayLocation">1004.3524169921875,343.38997173309326</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>ISO MPEG-4 Multiplexer 3</componentName>
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
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1249.30224609375,343.3900022506714</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="ROOT_audio1bitrate" path="../../audio1bitrate"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_audio1bitrate}bps_audio.MP4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_audio1bitrate}bps_audio.MP4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Manifest</componentName>
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
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">1484.8795776367188,189.5775022506714</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="file" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_manifest.ism"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_manifest.ism"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>AMS Manifest Writer</componentName>
                    <componentDefinitionName>AMSManifestWriter</componentDefinitionName>
                    <componentDefinitionGuid>3780304E-D2B1-4AA6-B109-893B4866DE5E</componentDefinitionGuid>
                    <componentOwningPluginName>AMSManifestWriter</componentOwningPluginName>
                    <componentOwningPluginId>com.imaginecommunications.AMSManifestWriter</componentOwningPluginId>
                    <childComponents/>
                    <pin name="writeComplete" type="EVENT"/>
                    <pin name="Asset 1" type="INPUT_PUSH">
                        <pinDefinition name="Asset 1" displayName="Asset 1" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 2" type="INPUT_PUSH">
                        <pinDefinition name="Asset 2" displayName="Asset 2" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 3" type="INPUT_PUSH">
                        <pinDefinition name="Asset 3" displayName="Asset 3" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 4" type="INPUT_PUSH">
                        <pinDefinition name="Asset 4" displayName="Asset 4" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="EndTime">60</property>
                    <property name="StartTime">15</property>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">486.6800231933594,6.6124725341796875</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="updateProgress">FALSE</property>
                    <componentName>Stream Trimmer</componentName>
                    <componentDefinitionName>Stream Trimmer</componentDefinitionName>
                    <componentDefinitionGuid>4EDCEFA6-93DE-463f-8C6B-543ED2CFCA77</componentDefinitionGuid>
                    <componentOwningPluginName>StreamSynchronizers</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.StreamSynchronizers</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                    <pin name="done" type="EVENT"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="EndTime" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_TrimmingEndTime" path="../TrimmingEndTime"/&gt;
    &lt;scriptString&gt;"${ROOT_TrimmingEndTime}"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_TrimmingEndTime}"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="StartTime" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_TrimmingStartTime" path="../TrimmingStartTime"/&gt;
    &lt;scriptString&gt;"${ROOT_TrimmingStartTime}"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_TrimmingStartTime}"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">377.17706298828125,549.0358276367188</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="updateProgress">FALSE</property>
                    <componentName>Stream Trimmer Audio</componentName>
                    <componentDefinitionName>Stream Trimmer</componentDefinitionName>
                    <componentDefinitionGuid>4EDCEFA6-93DE-463f-8C6B-543ED2CFCA77</componentDefinitionGuid>
                    <componentOwningPluginName>StreamSynchronizers</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.StreamSynchronizers</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                    <pin name="done" type="EVENT"/>
                </component>
            </childComponents>
            <pin name="primarySourceFile" type="PROPERTY">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">1.6775016784667969,37.93499755859375</property>
                <property name="_pinProperty">primarySourceFile</property>
            </pin>
            <pin name="clipListXml" type="PROPERTY">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">1.3549995422363281,15.902501106262207</property>
                <property name="_pinProperty">clipListXml</property>
            </pin>
            <pin name="outputAssetsCommand" type="COMMAND">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">1714.6722412109375,137.6900177001953</property>
            </pin>
        </component>
    </components>
    <pinConnections>
        <connection>
            <sourcePath>primarySourceFile</sourcePath>
            <destinationPath>Media File Input/filename</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 1/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 2/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Manifest/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>AMS Manifest Writer/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedVideo</sourcePath>
            <destinationPath>Stream Trimmer/in</destinationPath>
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
            <sourcePath>Stream Trimmer/out</sourcePath>
            <destinationPath>AVC Video Encoder 640x360 1200 kbps/Video</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 640x360 1200 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Speaker Position Assigner/out</sourcePath>
            <destinationPath>AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer/Track 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 2/Track 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 3/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio Stream Interleaver/out</sourcePath>
            <destinationPath>Stream Trimmer Audio/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Stream Trimmer Audio/out</sourcePath>
            <destinationPath>Speaker Position Assigner/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer/mp4</sourcePath>
            <destinationPath>File Output - Video 1/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 1/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Stream Trimmer/out</sourcePath>
            <destinationPath>AVC Video Encoder 960x540 2500 kbps/Video</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 960x540 2500 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 2/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 2/mp4</sourcePath>
            <destinationPath>File Output - Video 2/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 2/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 3/mp4</sourcePath>
            <destinationPath>File Output - Manifest/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Manifest/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 3</destinationPath>
        </connection>
    </pinConnections>
    <authoringInfo>
        <kayakFrameworkVersion>1.3.8.5</kayakFrameworkVersion>
        <userName>christoc</userName>
        <userLanguage>en</userLanguage>
        <platform>Windows</platform>
        <osName>Windows 8.1</osName>
        <osArch>amd64</osArch>
        <osVersion>6.3</osVersion>
        <authoredDate>2015-04-28T14:36:36.999+02:00</authoredDate>
        <plugins>
            <plugin pluginId="ca.digitalrapids.AACSourceController" name="AAC Source Controller">
                <pluginVersion>1.0.25.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-21 17:15:02-0400</buildDate>
                    <checksum>a37d1990c0a3ada3614cac9fa826c061</checksum>
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
                <pluginVersion>1.0.43.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-01-29 15:24:51-0500</buildDate>
                    <checksum>70bb602a13c07e15d8cd8525ba771003</checksum>
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
                <pluginVersion>1.0.71.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-12-16 15:44:15-0500</buildDate>
                    <checksum>f329c990ca3ee4be61defdf5eed30023</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="ClosedCaptionsUtilities">
                <pluginVersion>1.0.42.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-28 09:37:44-0500</buildDate>
                    <checksum>de68232249f90adbbed1a358576f386a</checksum>
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
                <pluginVersion>1.2.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-02-05 11:15:20-0500</buildDate>
                    <checksum>9bb778a8eefde87b9a8564cd87a844af</checksum>
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
                <pluginVersion>1.0.5.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-10-31 10:15:29-0400</buildDate>
                    <checksum>53a011ae355730c4b893d77f6021bd31</checksum>
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
                <pluginVersion>1.0.23.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-09 10:46:03-0400</buildDate>
                    <checksum>8ed8446b7e9e440d7fbb8fc1d8ab47b0</checksum>
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
                <pluginVersion>1.0.15.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-12 16:21:56-0500</buildDate>
                    <checksum>26afa82921298cd66939730070871292</checksum>
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
                <pluginVersion>1.4.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-01-09 17:49:31-0500</buildDate>
                    <checksum>b952a4514f7dedddef47e25ba54439fa</checksum>
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
                <pluginVersion>1.0.12.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-11 19:16:58-0500</buildDate>
                    <checksum>3dfdd49f8e071fe886e76060bde6d997</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonTimecode" name="CommonTimecode">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-06 15:04:52-0500</buildDate>
                    <checksum>0cdd77e7773cb540efb3cfc8396ca377</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonTimedText" name="CommonTimedText">
                <pluginVersion>1.0.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-11 19:16:43-0500</buildDate>
                    <checksum>bd7301c97546b6cd9e8a91df982358dd</checksum>
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
                <pluginVersion>1.2.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-12-10 09:13:36-0500</buildDate>
                    <checksum>4cf44f629189a2599586bb00a91029ee</checksum>
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
                <pluginVersion>1.0.84.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-27 10:54:12-0500</buildDate>
                    <checksum>916b5475ca9abbb5a9912712c61f0bc5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRColorspaceConverter" name="DRColorspaceConverter">
                <pluginVersion>1.2.7.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-16 16:25:31-0400</buildDate>
                    <checksum>e79b443db6ab20c50018a70085b3210b</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRDeinterlacer" name="DRDeinterlacer">
                <pluginVersion>1.4.14.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-16 16:25:50-0400</buildDate>
                    <checksum>aaa6c12ddc9c2a89651a7589b27a6e1f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRMediaProcessing" name="DRMediaProcessing">
                <pluginVersion>2.8.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-05 10:57:54-0500</buildDate>
                    <checksum>ad46ce281b53adbd0a43c6f8cea01014</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRProgressiveToInterlaced" name="DRProgressiveToInterlaced">
                <pluginVersion>1.1.12.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-28 18:22:47-0400</buildDate>
                    <checksum>d98200658bb56ed436b4dd914170548c</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRScaler" name="DRScaler">
                <pluginVersion>1.2.12.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-28 18:23:32-0400</buildDate>
                    <checksum>795f036b26f5900e8a22443e40a982dd</checksum>
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
                <pluginVersion>1.0.31.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-21 17:15:36-0400</buildDate>
                    <checksum>0e9d01dc02c8d5c3c867ccf5267393ae</checksum>
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
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-27 14:10:14-0500</buildDate>
                    <checksum>959423f723f6940e53046b2f835e2f97</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.KayakCore" name="KayakCore">
                <pluginVersion>1.3.8.5</pluginVersion>
                <buildInfo>
                    <buildDate>2014-12-15 15:31:37-0500</buildDate>
                    <checksum>af3160e2c8ce243da584f252a5b2d7f6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.KayakDesigner" name="KayakDesigner">
                <pluginVersion>1.3.8.1</pluginVersion>
                <buildInfo>
                    <buildDate>2015-01-13 16:36:31-0500</buildDate>
                    <checksum>f7b18d7ea6f07b3747b1e8160e25d891</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2AudioSourceController" name="MPEG2AudioSourceController">
                <pluginVersion>1.0.28.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-10-27 11:24:39-0400</buildDate>
                    <checksum>53087510bdcd4dbddca47524f2228c58</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2UDDemuxer">
                <pluginVersion>1.1.29.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-12-15 10:17:44-0500</buildDate>
                    <checksum>590506e3b8cf8f19d5bb734a165a8183</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2UDMuxer" name="MPEG2UDMuxer">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-01-02 12:00:07-0500</buildDate>
                    <checksum>c071ad556a0fb8c32e7cb10e602139f5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2VideoSourceController" name="MPEG2VideoSourceController">
                <pluginVersion>1.0.26.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-12 16:22:44-0500</buildDate>
                    <checksum>94495a9685a73447ce22e75a8ba1727c</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG4Muxer" name="MPEG4Muxer">
                <pluginVersion>1.1.64.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-06 15:19:52-0500</buildDate>
                    <checksum>7dc21939463462cfce229663ec7e2f2d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEGVideoDecoder" name="MPEGVideoDecoder">
                <pluginVersion>1.0.19.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-10-01 16:22:19-0400</buildDate>
                    <checksum>14f048fa056675fbd3d11e3c7b1214ec</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MXFDemuxer" name="MXFDemuxer">
                <pluginVersion>1.0.48.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-26 15:35:55-0400</buildDate>
                    <checksum>5f81f221b7812eb538c1157f3a1f5d8c</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaInspection" name="MediaInspection">
                <pluginVersion>1.0.48.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-06 15:17:21-0500</buildDate>
                    <checksum>91558c0d27738bab19175f23c85e40d0</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaManager" name="Media Manager">
                <pluginVersion>1.0.50.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-10-24 14:41:13-0400</buildDate>
                    <checksum>9142ffcc99e062fb667d7c8be5c3b4d7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaManagerWSClient" name="Media Manager WS Client">
                <pluginVersion>1.0.7.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-04 14:47:21-0500</buildDate>
                    <checksum>ba30fddfdeb81302c3ffd95cb767d5e8</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.StreamSynchronizers" name="StreamSynchronizers">
                <pluginVersion>1.0.43.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-27 19:11:48-0500</buildDate>
                    <checksum>3faf9925405d9fc450c9cb121890d7dd</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.TimecodeEncoder" name="TimecodeEncoder">
                <pluginVersion>1.0.28.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-10-29 15:10:43-0400</buildDate>
                    <checksum>62c8ae5790ed687f3f9a9f242c183820</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoBorder" name="VideoBorder">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-01-09 17:50:18-0500</buildDate>
                    <checksum>5ac189c161963e84f1a5d90b616da8b1</checksum>
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
                <pluginVersion>1.0.52.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-08 15:26:35-0400</buildDate>
                    <checksum>c231869fe0642f680c9993ce7d180cf6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFormatUtilities" name="VideoFormatUtilities">
                <pluginVersion>1.0.71.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-17 13:26:52-0400</buildDate>
                    <checksum>32204f71de7b878b284961f8c656c5d7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoProcessor" name="VideoProcessor">
                <pluginVersion>1.0.17.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-28 18:25:46-0400</buildDate>
                    <checksum>656f4b634bd4315b8f2289736c917211</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.AFDUtilities" name="AFDUtilities">
                <pluginVersion>1.1.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-01-06 09:37:21-0500</buildDate>
                    <checksum>37a5df166669960b36618a7d6516d3b9</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.AMSManifestWriter" name="AMSManifestWriter">
                <pluginVersion>1.0.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-03-10 17:03:05-0400</buildDate>
                    <checksum>244a5eab3a1e4002a1cbd6e9c300ad66</checksum>
                </buildInfo>
            </plugin>
        </plugins>
        <pluginIdentifiers>
            <plugin name="AAC Source Controller" buildDate="2014-08-21 17:15:02-0400" pluginId="ca.digitalrapids.AACSourceController" pluginVersion="1.0.25.0" platform="Windows" checksum="a37d1990c0a3ada3614cac9fa826c061"/>
            <plugin name="AC3 Source Controller" buildDate="2014-06-05 15:15:06-0400" pluginId="ca.digitalrapids.AC3SourceController" pluginVersion="1.0.28.0" platform="Windows" checksum="62e71f1beeef7aece22de090ba387568"/>
            <plugin name="AES3AudioProcessor" buildDate="2014-08-21 10:18:34-0400" pluginId="ca.digitalrapids.AES3AudioProcessor" pluginVersion="1.0.17.0" platform="Windows" checksum="ecd1dbfbdf4b5cc66ebfe984c4710466"/>
            <plugin name="AES3SourceController" buildDate="2014-08-18 14:51:13-0400" pluginId="ca.digitalrapids.AES3SourceController" pluginVersion="1.0.22.0" platform="Windows" checksum="b3f12b371c965d668d8319a9119403b7"/>
            <plugin name="AVC Source Controller" buildDate="2015-01-29 15:24:51-0500" pluginId="ca.digitalrapids.AVCSourceController" pluginVersion="1.0.43.0" platform="Windows" checksum="70bb602a13c07e15d8cd8525ba771003"/>
            <plugin name="Assets" buildDate="2013-10-18 16:43:49-0400" pluginId="ca.digitalrapids.Assets" pluginVersion="1.0.4.0" platform="Generic" checksum="255263cf15e1f4f2eedd6b3818774c5e"/>
            <plugin name="AudioFormatConverter" buildDate="2014-06-05 06:46:46-0400" pluginId="ca.digitalrapids.AudioFormatConverter" pluginVersion="1.0.15.0" platform="Generic" checksum="9600f0c46dbee32ddb35457459e78965"/>
            <plugin name="AudioFormatUtilities" buildDate="2014-12-16 15:44:15-0500" pluginId="ca.digitalrapids.AudioFormatUtilities" pluginVersion="1.0.71.0" platform="Windows" checksum="f329c990ca3ee4be61defdf5eed30023"/>
            <plugin name="ClosedCaptionsUtilities" buildDate="2014-11-28 09:37:44-0500" pluginId="ca.digitalrapids.ClosedCaptionsUtilities" pluginVersion="1.0.42.0" platform="Windows" checksum="de68232249f90adbbed1a358576f386a"/>
            <plugin name="CommonAAC" buildDate="2013-10-17 15:15:13-0400" pluginId="ca.digitalrapids.CommonAAC" pluginVersion="1.0.6.0" platform="Generic" checksum="ac4d12dbec71f08576aebabfd714e796"/>
            <plugin name="CommonAC3" buildDate="2013-10-17 15:19:06-0400" pluginId="ca.digitalrapids.CommonAC3" pluginVersion="1.1.9.0" platform="Generic" checksum="5d89d2d67175f9f780957eb87758fdb9"/>
            <plugin name="CommonAES3" buildDate="2014-08-07 16:36:41-0400" pluginId="ca.digitalrapids.CommonAES3" pluginVersion="1.1.3.0" platform="Generic" checksum="a2e5b5f10dccddeebbf46747cfc4b273"/>
            <plugin name="CommonAVC" buildDate="2014-07-25 08:30:23-0400" pluginId="ca.digitalrapids.CommonAVC" pluginVersion="1.0.19.0" platform="Windows" checksum="fcb0f15f2ff405925f2482ad19995b63"/>
            <plugin name="CommonComponents" buildDate="2015-02-05 11:15:20-0500" pluginId="ca.digitalrapids.CommonComponents" pluginVersion="1.2.3.0" platform="Generic" checksum="9bb778a8eefde87b9a8564cd87a844af"/>
            <plugin name="CommonDTS" buildDate="2013-10-18 15:01:07-0400" pluginId="ca.digitalrapids.CommonDTS" pluginVersion="1.0.4.0" platform="Generic" checksum="61f3136c4769493132791ab20eec14d2"/>
            <plugin name="CommonDV" buildDate="2014-10-31 10:15:29-0400" pluginId="ca.digitalrapids.CommonDV" pluginVersion="1.0.5.0" platform="Generic" checksum="53a011ae355730c4b893d77f6021bd31"/>
            <plugin name="CommonDolbyE" buildDate="2013-10-18 14:48:14-0400" pluginId="ca.digitalrapids.CommonDolbyE" pluginVersion="1.0.3.0" platform="Generic" checksum="8d9db933132fff45f77d1cca159caca3"/>
            <plugin name="CommonEAC3" buildDate="2013-10-18 15:10:24-0400" pluginId="ca.digitalrapids.CommonEAC3" pluginVersion="1.0.6.0" platform="Generic" checksum="4c4b4e50362a99bd36be9cb33d8a90ea"/>
            <plugin name="CommonFont" buildDate="2014-02-21 19:27:37-0500" pluginId="ca.digitalrapids.CommonFont" pluginVersion="1.0.1.0" platform="Generic" checksum="385df375fa90cde4af26294936172be7"/>
            <plugin name="CommonImageFormats" buildDate="2014-09-09 10:46:03-0400" pluginId="ca.digitalrapids.CommonImageFormats" pluginVersion="1.0.23.0" platform="Generic" checksum="8ed8446b7e9e440d7fbb8fc1d8ab47b0"/>
            <plugin name="CommonIntelIPP" buildDate="2013-10-18 15:39:11-0400" pluginId="ca.digitalrapids.CommonIntelIPP" pluginVersion="1.0.11.0" platform="Windows" checksum="e6f3740379a0221f52a423b40710dc08"/>
            <plugin name="CommonJ2KVideo" buildDate="2013-10-21 08:10:17-0400" pluginId="ca.digitalrapids.CommonJ2KVideo" pluginVersion="1.0.3.0" platform="Generic" checksum="bdf83e122eb86d00a3aeb8e42ff70fa2"/>
            <plugin name="CommonLanguage" buildDate="2014-06-04 21:58:14-0400" pluginId="ca.digitalrapids.CommonLanguage" pluginVersion="1.0.16.0" platform="Windows" checksum="d7c19cf7c0d9bc0762ad70b66dee7eb5"/>
            <plugin name="CommonMPEG" buildDate="2013-10-21 13:13:48-0400" pluginId="ca.digitalrapids.CommonMPEG" pluginVersion="1.0.4.0" platform="Generic" checksum="a934a85eb4d0c86a757d80a792ae032c"/>
            <plugin name="CommonMPEG1" buildDate="2013-10-21 13:16:50-0400" pluginId="ca.digitalrapids.CommonMPEG1" pluginVersion="1.0.4.0" platform="Generic" checksum="205521ae64fe69e65ef74070b07d321e"/>
            <plugin name="CommonMPEG2" buildDate="2014-11-12 16:21:56-0500" pluginId="ca.digitalrapids.CommonMPEG2" pluginVersion="1.0.15.0" platform="Generic" checksum="26afa82921298cd66939730070871292"/>
            <plugin name="CommonMPEG4" buildDate="2013-10-21 13:26:02-0400" pluginId="ca.digitalrapids.CommonMPEG4" pluginVersion="1.0.11.0" platform="Generic" checksum="ece896a89240b90c772bf89fb8179658"/>
            <plugin name="CommonMXF" buildDate="2013-10-21 13:28:57-0400" pluginId="ca.digitalrapids.CommonMXF" pluginVersion="1.0.3.0" platform="Generic" checksum="7cb12460dc147b18de8b15254e47df2a"/>
            <plugin name="CommonMedia" buildDate="2015-01-09 17:49:31-0500" pluginId="ca.digitalrapids.CommonMedia" pluginVersion="1.4.1.0" platform="Windows" checksum="b952a4514f7dedddef47e25ba54439fa"/>
            <plugin name="CommonMediaEncryption" buildDate="2014-06-27 17:24:40-0400" pluginId="ca.digitalrapids.CommonMediaEncryption" pluginVersion="1.0.7.0" platform="Generic" checksum="e8137cd8314ceb3134bd2569645d0cd2"/>
            <plugin name="CommonMetadata" buildDate="2013-10-21 12:54:21-0400" pluginId="ca.digitalrapids.CommonMetadata" pluginVersion="1.0.5.0" platform="Generic" checksum="69fc09d4a2979ccfd562a4ef693a60d7"/>
            <plugin name="CommonPlayReadyEncryption" buildDate="2013-10-21 13:59:20-0400" pluginId="ca.digitalrapids.CommonPlayReadyEncryption" pluginVersion="1.0.5.0" platform="Generic" checksum="f612be0829038b2d63b80d948dff3ee8"/>
            <plugin name="CommonQuickTime" buildDate="2013-10-21 15:35:15-0400" pluginId="ca.digitalrapids.CommonQuickTime" pluginVersion="1.0.3.0" platform="Generic" checksum="2f8c5ead8bfb9e7f614f6c9ae9684166"/>
            <plugin name="CommonStereoVideo" buildDate="2013-10-21 15:48:17-0400" pluginId="ca.digitalrapids.CommonStereoVideo" pluginVersion="1.0.2.0" platform="Generic" checksum="f1f78354a42e6e4a6a3aa17fa3be846f"/>
            <plugin name="CommonSubtitles" buildDate="2014-11-11 19:16:58-0500" pluginId="ca.digitalrapids.CommonSubtitles" pluginVersion="1.0.12.0" platform="Generic" checksum="3dfdd49f8e071fe886e76060bde6d997"/>
            <plugin name="CommonTimecode" buildDate="2013-11-06 15:04:52-0500" pluginId="ca.digitalrapids.CommonTimecode" pluginVersion="1.0.11.0" platform="Generic" checksum="0cdd77e7773cb540efb3cfc8396ca377"/>
            <plugin name="CommonTimedText" buildDate="2014-11-11 19:16:43-0500" pluginId="ca.digitalrapids.CommonTimedText" pluginVersion="1.0.2.0" platform="Generic" checksum="bd7301c97546b6cd9e8a91df982358dd"/>
            <plugin name="CommonUltraviolet" buildDate="2014-03-25 11:15:24-0400" pluginId="ca.digitalrapids.CommonUltraviolet" pluginVersion="1.0.3.0" platform="Generic" checksum="0fbaf01cd371c81f4fcb7cf66f41f6d9"/>
            <plugin name="CommonVC3" buildDate="2013-11-06 15:27:08-0500" pluginId="ca.digitalrapids.CommonVC3" pluginVersion="1.0.3.0" platform="Generic" checksum="ada6c958950fadf2780b72caac6bd90c"/>
            <plugin name="CommonVideoSystem" buildDate="2014-12-10 09:13:36-0500" pluginId="ca.digitalrapids.CommonVideoSystem" pluginVersion="1.2.1.0" platform="Windows" checksum="4cf44f629189a2599586bb00a91029ee"/>
            <plugin name="CommonWAVE" buildDate="2013-10-21 17:02:27-0400" pluginId="ca.digitalrapids.CommonWAVE" pluginVersion="1.0.3.0" platform="Generic" checksum="df370b1fb4c0a1c420d8c1385c428223"/>
            <plugin name="DRAVCEncoder" buildDate="2014-11-27 10:54:12-0500" pluginId="ca.digitalrapids.DRAVCEncoder" pluginVersion="1.0.84.0" platform="Windows" checksum="916b5475ca9abbb5a9912712c61f0bc5"/>
            <plugin name="DRColorspaceConverter" buildDate="2014-09-16 16:25:31-0400" pluginId="ca.digitalrapids.DRColorspaceConverter" pluginVersion="1.2.7.0" platform="Windows" checksum="e79b443db6ab20c50018a70085b3210b"/>
            <plugin name="DRDeinterlacer" buildDate="2014-09-16 16:25:50-0400" pluginId="ca.digitalrapids.DRDeinterlacer" pluginVersion="1.4.14.0" platform="Windows" checksum="aaa6c12ddc9c2a89651a7589b27a6e1f"/>
            <plugin name="DRMediaProcessing" buildDate="2014-11-05 10:57:54-0500" pluginId="ca.digitalrapids.DRMediaProcessing" pluginVersion="2.8.1.0" platform="Windows" checksum="ad46ce281b53adbd0a43c6f8cea01014"/>
            <plugin name="DRProgressiveToInterlaced" buildDate="2014-08-28 18:22:47-0400" pluginId="ca.digitalrapids.DRProgressiveToInterlaced" pluginVersion="1.1.12.0" platform="Windows" checksum="d98200658bb56ed436b4dd914170548c"/>
            <plugin name="DRScaler" buildDate="2014-08-28 18:23:32-0400" pluginId="ca.digitalrapids.DRScaler" pluginVersion="1.2.12.0" platform="Windows" checksum="795f036b26f5900e8a22443e40a982dd"/>
            <plugin name="DTS Source Controller" buildDate="2013-11-01 15:22:20-0400" pluginId="ca.digitalrapids.DTSSourceController" pluginVersion="1.0.12.0" platform="Windows" checksum="374aaf0ece6f5fecefc8c73de24a6f3e"/>
            <plugin name="DirectShowFileSource" buildDate="2013-11-20 21:17:46-0500" pluginId="ca.digitalrapids.DirectShowFileSource" pluginVersion="1.0.19.0" platform="Windows" checksum="e35fae50cdcf706cb794dd1bd14b35cf"/>
            <plugin name="Dolby E Source Controller" buildDate="2014-08-07 16:37:41-0400" pluginId="ca.digitalrapids.DolbyESourceController" pluginVersion="1.0.6.0" platform="Windows" checksum="857e18a3b26edee7a84f8eae5f741178"/>
            <plugin name="DolbyPulseEncoder" buildDate="2014-08-21 17:15:36-0400" pluginId="ca.digitalrapids.DolbyPulseEncoder" pluginVersion="1.0.31.0" platform="Windows" checksum="0e9d01dc02c8d5c3c867ccf5267393ae"/>
            <plugin name="EAC3 Source Controller" buildDate="2013-11-01 15:50:54-0400" pluginId="ca.digitalrapids.EAC3SourceController" pluginVersion="1.0.12.0" platform="Windows" checksum="c237507b50e8d6842ffb8e24af37a314"/>
            <plugin name="EIACaptionsRetimer" buildDate="2014-11-27 14:10:14-0500" pluginId="ca.digitalrapids.EIACaptionsRetimer" pluginVersion="1.1.1.0" platform="Windows" checksum="959423f723f6940e53046b2f835e2f97"/>
            <plugin name="KayakCore" buildDate="2014-12-15 15:31:37-0500" pluginId="ca.digitalrapids.KayakCore" pluginVersion="1.3.8.5" platform="Windows" checksum="af3160e2c8ce243da584f252a5b2d7f6"/>
            <plugin name="KayakDesigner" buildDate="2015-01-13 16:36:31-0500" pluginId="ca.digitalrapids.KayakDesigner" pluginVersion="1.3.8.1" platform="Generic" checksum="f7b18d7ea6f07b3747b1e8160e25d891"/>
            <plugin name="MPEG2AudioSourceController" buildDate="2014-10-27 11:24:39-0400" pluginId="ca.digitalrapids.MPEG2AudioSourceController" pluginVersion="1.0.28.0" platform="Windows" checksum="53087510bdcd4dbddca47524f2228c58"/>
            <plugin name="MPEG2UDDemuxer" buildDate="2014-12-15 10:17:44-0500" pluginId="ca.digitalrapids.MPEG2UDDemuxer" pluginVersion="1.1.29.0" platform="Windows" checksum="590506e3b8cf8f19d5bb734a165a8183"/>
            <plugin name="MPEG2UDMuxer" buildDate="2015-01-02 12:00:07-0500" pluginId="ca.digitalrapids.MPEG2UDMuxer" pluginVersion="1.1.1.0" platform="Windows" checksum="c071ad556a0fb8c32e7cb10e602139f5"/>
            <plugin name="MPEG2VideoSourceController" buildDate="2014-11-12 16:22:44-0500" pluginId="ca.digitalrapids.MPEG2VideoSourceController" pluginVersion="1.0.26.0" platform="Windows" checksum="94495a9685a73447ce22e75a8ba1727c"/>
            <plugin name="MPEG4Muxer" buildDate="2014-11-06 15:19:52-0500" pluginId="ca.digitalrapids.MPEG4Muxer" pluginVersion="1.1.64.0" platform="Windows" checksum="7dc21939463462cfce229663ec7e2f2d"/>
            <plugin name="MPEGVideoDecoder" buildDate="2014-10-01 16:22:19-0400" pluginId="ca.digitalrapids.MPEGVideoDecoder" pluginVersion="1.0.19.0" platform="Windows" checksum="14f048fa056675fbd3d11e3c7b1214ec"/>
            <plugin name="MXFDemuxer" buildDate="2014-08-26 15:35:55-0400" pluginId="ca.digitalrapids.MXFDemuxer" pluginVersion="1.0.48.0" platform="Windows" checksum="5f81f221b7812eb538c1157f3a1f5d8c"/>
            <plugin name="MediaInspection" buildDate="2014-11-06 15:17:21-0500" pluginId="ca.digitalrapids.MediaInspection" pluginVersion="1.0.48.0" platform="Generic" checksum="91558c0d27738bab19175f23c85e40d0"/>
            <plugin name="Media Manager" buildDate="2014-10-24 14:41:13-0400" pluginId="ca.digitalrapids.MediaManager" pluginVersion="1.0.50.0" platform="Generic" checksum="9142ffcc99e062fb667d7c8be5c3b4d7"/>
            <plugin name="Media Manager WS Client" buildDate="2013-11-04 14:47:21-0500" pluginId="ca.digitalrapids.MediaManagerWSClient" pluginVersion="1.0.7.0" platform="Generic" checksum="ba30fddfdeb81302c3ffd95cb767d5e8"/>
            <plugin name="StreamSynchronizers" buildDate="2014-11-27 19:11:48-0500" pluginId="ca.digitalrapids.StreamSynchronizers" pluginVersion="1.0.43.0" platform="Windows" checksum="3faf9925405d9fc450c9cb121890d7dd"/>
            <plugin name="TimecodeEncoder" buildDate="2014-10-29 15:10:43-0400" pluginId="ca.digitalrapids.TimecodeEncoder" pluginVersion="1.0.28.0" platform="Windows" checksum="62c8ae5790ed687f3f9a9f242c183820"/>
            <plugin name="VideoBorder" buildDate="2015-01-09 17:50:18-0500" pluginId="ca.digitalrapids.VideoBorder" pluginVersion="1.1.1.0" platform="Windows" checksum="5ac189c161963e84f1a5d90b616da8b1"/>
            <plugin name="VideoDeinterlacers" buildDate="2013-10-23 14:22:16-0400" pluginId="ca.digitalrapids.VideoDeinterlacers" pluginVersion="1.0.11.0" platform="Windows" checksum="b81fd23b4334dbdba1b981c9577f82e5"/>
            <plugin name="VideoFormatConverter" buildDate="2014-09-08 15:26:35-0400" pluginId="ca.digitalrapids.VideoFormatConverter" pluginVersion="1.0.52.0" platform="Generic" checksum="c231869fe0642f680c9993ce7d180cf6"/>
            <plugin name="VideoFormatUtilities" buildDate="2014-09-17 13:26:52-0400" pluginId="ca.digitalrapids.VideoFormatUtilities" pluginVersion="1.0.71.0" platform="Windows" checksum="32204f71de7b878b284961f8c656c5d7"/>
            <plugin name="VideoProcessor" buildDate="2014-08-28 18:25:46-0400" pluginId="ca.digitalrapids.VideoProcessor" pluginVersion="1.0.17.0" platform="Windows" checksum="656f4b634bd4315b8f2289736c917211"/>
            <plugin name="AFDUtilities" buildDate="2015-01-06 09:37:21-0500" pluginId="com.imaginecommunications.AFDUtilities" pluginVersion="1.1.2.0" platform="Windows" checksum="37a5df166669960b36618a7d6516d3b9"/>
            <plugin name="AMSManifestWriter" buildDate="2015-03-10 17:03:05-0400" pluginId="com.imaginecommunications.AMSManifestWriter" pluginVersion="1.0.1.0" platform="Generic" checksum="244a5eab3a1e4002a1cbd6e9c300ad66"/>
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
            <plugin pluginId="ca.digitalrapids.CommonTimedText" name="CommonTimedText"/>
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
            <plugin pluginId="ca.digitalrapids.StreamSynchronizers" name="StreamSynchronizers"/>
            <plugin pluginId="ca.digitalrapids.TimecodeEncoder" name="TimecodeEncoder"/>
            <plugin pluginId="ca.digitalrapids.VideoBorder" name="VideoBorder"/>
            <plugin pluginId="ca.digitalrapids.VideoDeinterlacers" name="VideoDeinterlacers"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter" name="VideoFormatConverter"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatUtilities" name="VideoFormatUtilities"/>
            <plugin pluginId="ca.digitalrapids.VideoProcessor" name="VideoProcessor"/>
            <plugin pluginId="com.imaginecommunications.AFDUtilities" name="AFDUtilities"/>
            <plugin pluginId="com.imaginecommunications.AMSManifestWriter" name="AMSManifestWriter"/>
        </plugins>
        <components>
            <component pluginId="ca.digitalrapids.AACSourceController" name="AAC ADTS to Raw Converter" guid="eed0ba59-346f-47b0-ba9a-2ea14be6fa53"/>
            <component pluginId="ca.digitalrapids.AACSourceController" name="AAC Controller" guid="784ee2cc-8a15-41c9-b84b-1a79ced4a646"/>
            <component pluginId="ca.digitalrapids.DolbyPulseEncoder" name="AAC Encoder - Dolby Pulse" displayName="AAC Encoder (Dolby)" guid="D0933A55-4818-4ADC-9301-8BE7687AC9E3"/>
            <component pluginId="ca.digitalrapids.DolbyPulseEncoder" name="Atomic AAC Encoder - Dolby Pulse" displayName="AAC Encoder Core (Dolby)" guid="8916cfea-3397-4310-b5bb-402e27fb0baf"/>
            <component pluginId="ca.digitalrapids.AES3SourceController" name="AES3 Controller" guid="27e78f33-8cf3-4b32-bb7c-03009984567f"/>
            <component pluginId="ca.digitalrapids.AES3SourceController" name="AES3 Media Inspector" guid="c1718874-b04c-48a6-a1c6-120bfb4a928a"/>
            <component pluginId="com.imaginecommunications.AMSManifestWriter" name="AMSManifestWriter" displayName="AMS Manifest Writer" guid="3780304E-D2B1-4AA6-B109-893B4866DE5E"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Audio Container Data Type Merger" guid="3AED3909-CEC8-4413-BF58-33FA08514D0C"/>
            <component pluginId="ca.digitalrapids.AudioFormatConverter" name="Audio Format Converter" guid="F2A4515C-ABD5-49f9-B0D5-DB462E4BB674"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Audio Stream Interleaver" guid="D166D48B-FA26-44ca-8F2D-62B20D892659"/>
            <component pluginId="ca.digitalrapids.AVCSourceController" name="AVC Controller" guid="6ae5cf5f-3a25-4f61-8e3c-16c33b474d4c"/>
            <component pluginId="ca.digitalrapids.AVCSourceController" name="AVC Part10 to Part15 Converter" guid="b2eC0208-f841-4272-8a16-4b88e80d86a5"/>
            <component pluginId="ca.digitalrapids.DRAVCEncoder" name="Advanced AVC Encoder" displayName="AVC Video Encoder" guid="A3597472-D51E-44d9-9F0A-395744A83FA3"/>
            <component pluginId="ca.digitalrapids.DRAVCEncoder" name="AVC Encoder" displayName="AVC Video Encoder Core" guid="16c55dc4-7cd8-4d25-bfec-1cc4aebad739"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Bit Depth Converter" guid="7DF81BC0-6DFD-44fd-BDAA-2E568F65CFF6"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Channel Mapper" displayName="Channel Remapper" guid="771ACEB1-E611-4803-A356-21F221E3753D"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Color Space Converter - Intel" displayName="Color Space Converter - Intel" guid="2FDE07E0-7DBF-47e2-BC73-91F3B82D4392"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Container Data Type Merger" guid="b6eac4c1-3c04-4f8d-9654-96da605b9e90"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Data Type Merger" guid="4971c1a4-07ab-4c9a-93a6-947526a1005d"/>
            <component pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="EIA-708 to 608 Converter" displayName="EIA-708 to 608 De-Embedder" guid="57CA8716-84CF-4C7F-B59F-DF34AFE2E73E"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="EndOfStreamNotification" displayName="End of Stream Notification" guid="285BF6A1-3FEA-4c2a-9D2D-4DB4B965C3EA"/>
            <component pluginId="ca.digitalrapids.CommonMedia" name="Endian Converter" guid="D076A34F-6E7D-46BD-875A-4C590B5538BF"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="File Output" guid="9b376163-de8d-4e09-8bed-353725b6b6d6"/>
            <component pluginId="ca.digitalrapids.MPEG4Muxer" name="Advanced ISO MPEG4 Multiplexer" displayName="ISO MPEG-4 Multiplexer" guid="E25468C3-A65C-4f1a-8172-E72CE4B63A70"/>
            <component pluginId="ca.digitalrapids.MPEG4Muxer" name="ISO MPEG4 Multiplexer" displayName="ISO MPEG-4 Multiplexer Core" guid="3CC47644-DC6D-4f2b-AB3B-580D305F47CC"/>
            <component pluginId="ca.digitalrapids.CommonMedia" name="Media Data Type Auto Updater" guid="9dc80c38-b4ff-4b3e-8324-2f29abeb461e"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media File Input" guid="7cec6ecd-a477-4834-bc6f-97e34aa58bb5"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media Inpection Data Type Merger" guid="A025A4BD-A59D-42e4-B00C-66F67BCB147C"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media Inspector" guid="3ada68f0-f492-4133-87e2-cdb55ae9f7fc"/>
            <component pluginId="ca.digitalrapids.MPEG2VideoSourceController" name="MPEG2 Video Controller" displayName="MPEG Video Controller" guid="232820c7-05c5-4938-b92e-19798db53a3c"/>
            <component pluginId="ca.digitalrapids.MPEGVideoDecoder" name="MPEG Video Decoder" displayName="MPEG Video Decoder" guid="63701505-b844-4f9e-8077-81065262388d"/>
            <component pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2 User Data Decoder" displayName="MPEG-2 User Data Decoder" guid="abc91f15-8728-463d-92c3-84a158b24248"/>
            <component pluginId="ca.digitalrapids.MPEG2VideoSourceController" name="MPEG2 Video Media Inspector" displayName="MPEG-2 Video Media Inspector" guid="c920fef1-a139-477f-a630-bfc31ecf9d9c"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Multi-Program Audio Splitter" displayName="Multi-Program Audio Splitter" guid="6436a63f-1fa4-40e6-ba86-95138130d456"/>
            <component pluginId="ca.digitalrapids.MXFDemuxer" name="MXF Demultiplexer" guid="1A6B42BD-8131-41c3-8E51-361AB75A08B5"/>
            <component pluginId="ca.digitalrapids.MXFDemuxer" name="MXF Media Inspector" guid="C1261FE2-7506-4e7d-A6D7-5F576F723B2A"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Random Access File" guid="ef0bd6fd-7564-4efb-bb78-a184bce33a29"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Raw Audio Data Type Merger" guid="08D76F09-6818-4214-B4CF-0E7591556ADE"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Raw Audio Media Inspector" guid="F16BE80D-2AAB-4126-8820-1E05F64FB99D"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Raw Audio Processor" guid="9C0D7AA4-45A5-4561-B6EB-BCA2E0D4856F"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Sample Rate Converter" guid="0DAC861A-FDD8-4e0c-97BB-3341C4E46999"/>
            <component pluginId="ca.digitalrapids.DRScaler" name="DRScaler" displayName="Scaler" guid="2EA57BB6-D100-4eaf-8DE0-1739BD64B833"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Speaker Position Assigner" guid="AB851938-A3DA-4062-9F4A-FB8AF260D887"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="Stream Trimmer" guid="4EDCEFA6-93DE-463f-8C6B-543ED2CFCA77"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="Stream Truncator" guid="3B8118A9-72E6-42b7-910A-014D9E8C1575"/>
            <component pluginId="ca.digitalrapids.MediaManager" name="Transcode Task Graph" displayName="Transcode Blueprint" guid="cc2f8f8a-85a3-4522-85a5-b0b26b12f4cd"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="Trimming Validator" guid="6D3E7814-6954-4e57-BF9C-AC843726A621"/>
            <component pluginId="ca.digitalrapids.VideoFormatConverter" name="Video Format Converter" displayName="Video Format Converter (Deprecated)" guid="AC185E0C-6839-4dae-A547-5E18DF5EA058"/>
            <component pluginId="ca.digitalrapids.KayakCore" name="Kayak Graph" displayName="Zenium Graph" guid="abc785f2-427e-4522-ba00-f3cb6acd1220"/>
            <component pluginId="ca.digitalrapids.KayakCore" name="Kayak OOP Graph" displayName="Zenium OOP Graph" guid="967a0d59-a62e-4c75-962c-4f65c180d45c"/>
        </components>
    </dependencyInfo>
</kayakDocument>
