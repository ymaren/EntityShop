namespace Core.Dal.AdoNet.Helpers
{
    using System;
    using System.Data;

    interface IPropertyMapper<TType>
    {
        void Map(IDataReader reader, TType entity);
    }

    class PropertyMapper<TType> : IPropertyMapper<TType>
    {
        private readonly string _fieldName;
        private readonly Action<object, TType> _mapper;

        public PropertyMapper(string field, Action<object, TType> mapper)
        {
            _fieldName = field;
            _mapper = mapper;
        }

        public void Map(IDataReader reader, TType entity)
        {
            var value = reader[_fieldName];
            _mapper(value, entity);
        }
    }
}
