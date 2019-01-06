﻿using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
namespace DevExpress.Persistent.BaseImpl.EF
{
    [NavigationItem("Main")]
    public class H2Category : IHCategory
    {
        public H2Category()
        {
            Children = new BindingList<H2Category>();
        }
        [Browsable(false)]
        public Int32 ID { get; protected set; }
        public String Name { get; set; }
        
        public H2Category Parent { get; set; }
       // [Aggregated]
        public virtual IList<H2Category> Children { get; set; }
        [NotMapped, Browsable(false), RuleFromBoolProperty("H2CategoryCircularReferences", DefaultContexts.Save, "Circular refrerence detected. To correct this error, set the Parent property to another value.", UsedProperties = "Parent")]
        public Boolean IsValid
        {
            get
            {
                H2Category currentObj = Parent;
                while (currentObj != null)
                {
                    if (currentObj == this)
                    {
                        return false;
                    }
                    currentObj = currentObj.Parent;
                }
                return true;
            }
        }
        IBindingList ITreeNode.Children => Children as IBindingList;

        ITreeNode IHCategory.Parent
        {
            get => Parent as IHCategory;
            set => Parent = value as H2Category;
        }
        ITreeNode ITreeNode.Parent => Parent as ITreeNode;
    }
}