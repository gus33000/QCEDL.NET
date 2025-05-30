﻿namespace Qualcomm.EmergencyDownload.Layers.APSS.Firehose.JSON.StorageInfo
{
    public class StorageInfo
    {
        public int total_blocks
        {
            get; set;
        }
        public int block_size
        {
            get; set;
        }
        public int page_size
        {
            get; set;
        }
        public int num_physical
        {
            get; set;
        }
        public int manufacturer_id
        {
            get; set;
        }
        public int serial_num
        {
            get; set;
        }
        public string fw_version
        {
            get; set;
        }
        public string mem_type
        {
            get; set;
        }
        public string prod_name
        {
            get; set;
        }
    }
}
