﻿using System.Xml.Serialization;

namespace EDLTests.Qualcomm.EmergencyDownload.Firehose.Xml.Elements
{
    public class Configure
    {
        private StorageType? memoryName;

        [XmlAttribute(AttributeName = "MemoryName")]
        public StorageType MemoryName
        {
            get => memoryName ?? StorageType.UFS; set => memoryName = value;
        }

        public bool ShouldSerializeMemoryName()
        {
            return memoryName.HasValue;
        }

        private string? verbose;

        [XmlAttribute(AttributeName = "Verbose")]
        public string Verbose
        {
            get => verbose ?? "0"; set => verbose = value;
        }

        public bool ShouldSerializeVerbose()
        {
            return verbose != null;
        }

        private ulong? maxPayloadSizeToTargetInBytes;

        [XmlAttribute(AttributeName = "MaxPayloadSizeToTargetInBytes")]
        public ulong MaxPayloadSizeToTargetInBytes
        {
            get => maxPayloadSizeToTargetInBytes ?? 0; set => maxPayloadSizeToTargetInBytes = value;
        }

        public bool ShouldSerializeMaxPayloadSizeToTargetInBytes()
        {
            return maxPayloadSizeToTargetInBytes.HasValue;
        }

        private string? alwaysValidate;

        [XmlAttribute(AttributeName = "AlwaysValidate")]
        public string AlwaysValidate
        {
            get => alwaysValidate ?? "0"; set => alwaysValidate = value;
        }

        public bool ShouldSerializeAlwaysValidate()
        {
            return alwaysValidate != null;
        }

        private ulong? maxDigestTableSizeInBytes;

        [XmlAttribute(AttributeName = "MaxDigestTableSizeInBytes")]
        public ulong MaxDigestTableSizeInBytes
        {
            get => maxDigestTableSizeInBytes ?? 0; set => maxDigestTableSizeInBytes = value;
        }

        public bool ShouldSerializeMaxDigestTableSizeInBytes()
        {
            return maxDigestTableSizeInBytes.HasValue;
        }

        private string? zlpAwareHost;

        [XmlAttribute(AttributeName = "ZlpAwareHost")]
        public string ZlpAwareHost
        {
            get => zlpAwareHost ?? "1"; set => zlpAwareHost = value;
        }

        public bool ShouldSerializeZlpAwareHost()
        {
            return zlpAwareHost != null;
        }

        private string? skipWrite;

        [XmlAttribute(AttributeName = "SkipWrite")]
        public string SkipWrite
        {
            get => skipWrite ?? "0"; set => skipWrite = value;
        }

        public bool ShouldSerializeSkipWrite()
        {
            return skipWrite != null;
        }
    }
}
