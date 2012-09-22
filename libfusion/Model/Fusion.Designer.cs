﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("Model", "FK_files_0_0", "packages", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Fusion.Framework.Model.Package), "files", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Fusion.Framework.Model.File), true)]
[assembly: EdmRelationshipAttribute("Model", "FK_metadata_0_0", "Package", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Fusion.Framework.Model.Package), "metadata", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Fusion.Framework.Model.MetadataItem), true)]

#endregion

namespace Fusion.Framework.Model
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    internal partial class Entities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new Entities object using the connection string found in the 'Entities' section of the application configuration file.
        /// </summary>
        public Entities() : base("name=Entities", "Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Entities object.
        /// </summary>
        public Entities(string connectionString) : base(connectionString, "Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Entities object.
        /// </summary>
        public Entities(EntityConnection connection) : base(connection, "Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<File> Files
        {
            get
            {
                if ((_Files == null))
                {
                    _Files = base.CreateObjectSet<File>("Files");
                }
                return _Files;
            }
        }
        private ObjectSet<File> _Files;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Package> Packages
        {
            get
            {
                if ((_Packages == null))
                {
                    _Packages = base.CreateObjectSet<Package>("Packages");
                }
                return _Packages;
            }
        }
        private ObjectSet<Package> _Packages;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<WorldItem> WorldSet
        {
            get
            {
                if ((_WorldSet == null))
                {
                    _WorldSet = base.CreateObjectSet<WorldItem>("WorldSet");
                }
                return _WorldSet;
            }
        }
        private ObjectSet<WorldItem> _WorldSet;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<MetadataItem> Metadata
        {
            get
            {
                if ((_Metadata == null))
                {
                    _Metadata = base.CreateObjectSet<MetadataItem>("Metadata");
                }
                return _Metadata;
            }
        }
        private ObjectSet<MetadataItem> _Metadata;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<TrashItem> Trash
        {
            get
            {
                if ((_Trash == null))
                {
                    _Trash = base.CreateObjectSet<TrashItem>("Trash");
                }
                return _Trash;
            }
        }
        private ObjectSet<TrashItem> _Trash;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Files EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToFiles(File file)
        {
            base.AddObject("Files", file);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Packages EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToPackages(Package package)
        {
            base.AddObject("Packages", package);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the WorldSet EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToWorldSet(WorldItem worldItem)
        {
            base.AddObject("WorldSet", worldItem);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Metadata EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToMetadata(MetadataItem metadataItem)
        {
            base.AddObject("Metadata", metadataItem);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Trash EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTrash(TrashItem trashItem)
        {
            base.AddObject("Trash", trashItem);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="File")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class File : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new File object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="path">Initial value of the Path property.</param>
        /// <param name="type">Initial value of the Type property.</param>
        /// <param name="packageID">Initial value of the PackageID property.</param>
        public static File CreateFile(global::System.Int64 id, global::System.String path, global::System.Int64 type, global::System.Int64 packageID)
        {
            File file = new File();
            file.ID = id;
            file.Path = path;
            file.Type = type;
            file.PackageID = packageID;
            return file;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int64 _ID;
        partial void OnIDChanging(global::System.Int64 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Path
        {
            get
            {
                return _Path;
            }
            set
            {
                OnPathChanging(value);
                ReportPropertyChanging("Path");
                _Path = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Path");
                OnPathChanged();
            }
        }
        private global::System.String _Path;
        partial void OnPathChanging(global::System.String value);
        partial void OnPathChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 Type
        {
            get
            {
                return _Type;
            }
            set
            {
                OnTypeChanging(value);
                ReportPropertyChanging("Type");
                _Type = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Type");
                OnTypeChanged();
            }
        }
        private global::System.Int64 _Type;
        partial void OnTypeChanging(global::System.Int64 value);
        partial void OnTypeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Digest
        {
            get
            {
                return _Digest;
            }
            set
            {
                OnDigestChanging(value);
                ReportPropertyChanging("Digest");
                _Digest = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Digest");
                OnDigestChanged();
            }
        }
        private global::System.String _Digest;
        partial void OnDigestChanging(global::System.String value);
        partial void OnDigestChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 PackageID
        {
            get
            {
                return _PackageID;
            }
            set
            {
                OnPackageIDChanging(value);
                ReportPropertyChanging("PackageID");
                _PackageID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PackageID");
                OnPackageIDChanged();
            }
        }
        private global::System.Int64 _PackageID;
        partial void OnPackageIDChanging(global::System.Int64 value);
        partial void OnPackageIDChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_files_0_0", "packages")]
        public Package Package
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Package>("Model.FK_files_0_0", "packages").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Package>("Model.FK_files_0_0", "packages").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Package> PackageReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Package>("Model.FK_files_0_0", "packages");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Package>("Model.FK_files_0_0", "packages", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="MetadataItem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class MetadataItem : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new MetadataItem object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="key">Initial value of the Key property.</param>
        /// <param name="value">Initial value of the Value property.</param>
        /// <param name="packageID">Initial value of the PackageID property.</param>
        public static MetadataItem CreateMetadataItem(global::System.Int64 id, global::System.String key, global::System.String value, global::System.Int64 packageID)
        {
            MetadataItem metadataItem = new MetadataItem();
            metadataItem.ID = id;
            metadataItem.Key = key;
            metadataItem.Value = value;
            metadataItem.PackageID = packageID;
            return metadataItem;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int64 _ID;
        partial void OnIDChanging(global::System.Int64 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Key
        {
            get
            {
                return _Key;
            }
            set
            {
                OnKeyChanging(value);
                ReportPropertyChanging("Key");
                _Key = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Key");
                OnKeyChanged();
            }
        }
        private global::System.String _Key;
        partial void OnKeyChanging(global::System.String value);
        partial void OnKeyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Value
        {
            get
            {
                return _Value;
            }
            set
            {
                OnValueChanging(value);
                ReportPropertyChanging("Value");
                _Value = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Value");
                OnValueChanged();
            }
        }
        private global::System.String _Value;
        partial void OnValueChanging(global::System.String value);
        partial void OnValueChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 PackageID
        {
            get
            {
                return _PackageID;
            }
            set
            {
                OnPackageIDChanging(value);
                ReportPropertyChanging("PackageID");
                _PackageID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PackageID");
                OnPackageIDChanged();
            }
        }
        private global::System.Int64 _PackageID;
        partial void OnPackageIDChanging(global::System.Int64 value);
        partial void OnPackageIDChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_metadata_0_0", "Package")]
        public Package Package
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Package>("Model.FK_metadata_0_0", "Package").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Package>("Model.FK_metadata_0_0", "Package").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Package> PackageReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Package>("Model.FK_metadata_0_0", "Package");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Package>("Model.FK_metadata_0_0", "Package", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Package")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Package : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Package object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="fullName">Initial value of the FullName property.</param>
        /// <param name="version">Initial value of the Version property.</param>
        /// <param name="slot">Initial value of the Slot property.</param>
        public static Package CreatePackage(global::System.Int64 id, global::System.String fullName, global::System.String version, global::System.Int64 slot)
        {
            Package package = new Package();
            package.ID = id;
            package.FullName = fullName;
            package.Version = version;
            package.Slot = slot;
            return package;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int64 _ID;
        partial void OnIDChanging(global::System.Int64 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                OnFullNameChanging(value);
                ReportPropertyChanging("FullName");
                _FullName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("FullName");
                OnFullNameChanged();
            }
        }
        private global::System.String _FullName;
        partial void OnFullNameChanging(global::System.String value);
        partial void OnFullNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Version
        {
            get
            {
                return _Version;
            }
            set
            {
                OnVersionChanging(value);
                ReportPropertyChanging("Version");
                _Version = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Version");
                OnVersionChanged();
            }
        }
        private global::System.String _Version;
        partial void OnVersionChanging(global::System.String value);
        partial void OnVersionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 Slot
        {
            get
            {
                return _Slot;
            }
            set
            {
                OnSlotChanging(value);
                ReportPropertyChanging("Slot");
                _Slot = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Slot");
                OnSlotChanged();
            }
        }
        private global::System.Int64 _Slot;
        partial void OnSlotChanging(global::System.Int64 value);
        partial void OnSlotChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Project
        {
            get
            {
                return _Project;
            }
            set
            {
                OnProjectChanging(value);
                ReportPropertyChanging("Project");
                _Project = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Project");
                OnProjectChanged();
            }
        }
        private global::System.String _Project;
        partial void OnProjectChanging(global::System.String value);
        partial void OnProjectChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_files_0_0", "files")]
        public EntityCollection<File> Files
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<File>("Model.FK_files_0_0", "files");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<File>("Model.FK_files_0_0", "files", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_metadata_0_0", "metadata")]
        public EntityCollection<MetadataItem> Metadata
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<MetadataItem>("Model.FK_metadata_0_0", "metadata");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<MetadataItem>("Model.FK_metadata_0_0", "metadata", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="TrashItem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TrashItem : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TrashItem object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="path">Initial value of the Path property.</param>
        public static TrashItem CreateTrashItem(global::System.Int64 id, global::System.String path)
        {
            TrashItem trashItem = new TrashItem();
            trashItem.ID = id;
            trashItem.Path = path;
            return trashItem;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int64 _ID;
        partial void OnIDChanging(global::System.Int64 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Path
        {
            get
            {
                return _Path;
            }
            set
            {
                OnPathChanging(value);
                ReportPropertyChanging("Path");
                _Path = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Path");
                OnPathChanged();
            }
        }
        private global::System.String _Path;
        partial void OnPathChanging(global::System.String value);
        partial void OnPathChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="WorldItem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class WorldItem : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new WorldItem object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="atom">Initial value of the Atom property.</param>
        public static WorldItem CreateWorldItem(global::System.Int64 id, global::System.String atom)
        {
            WorldItem worldItem = new WorldItem();
            worldItem.ID = id;
            worldItem.Atom = atom;
            return worldItem;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int64 _ID;
        partial void OnIDChanging(global::System.Int64 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Atom
        {
            get
            {
                return _Atom;
            }
            set
            {
                OnAtomChanging(value);
                ReportPropertyChanging("Atom");
                _Atom = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Atom");
                OnAtomChanged();
            }
        }
        private global::System.String _Atom;
        partial void OnAtomChanging(global::System.String value);
        partial void OnAtomChanged();

        #endregion
    
    }

    #endregion
    
}
