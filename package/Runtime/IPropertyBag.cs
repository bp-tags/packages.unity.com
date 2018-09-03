﻿using System.Collections.Generic;

namespace Unity.Properties
{
    public interface IPropertyBag
    {
        int PropertyCount { get; }
        IEnumerable<IProperty> Properties { get; }
        IProperty FindProperty(string name);

        void Visit<TContainer>(TContainer container, IPropertyVisitor visitor) 
            where TContainer : class, IPropertyContainer;
        
        void Visit<TContainer>(ref TContainer container, IPropertyVisitor visitor) 
            where TContainer : struct, IPropertyContainer;
    }
}