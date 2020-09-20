using System;
using System.Collections.Generic;
using System.Linq;
using DowntimeAlerter.Domain.Entities;
using System.Linq.Expressions;

namespace DowntimeAlerter.Domain.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region Utilities

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(int id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, nameof(Entity.Id));

            Expression<Func<object>> closure = () => id;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        #endregion

        #region Select

        public abstract IQueryable<TEntity> GetAll();

        public virtual List<TEntity> GetAllList() => GetAll().ToList();

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate) => GetAll().Where(predicate).ToList();

        public virtual TEntity Get(int id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
                throw new Exception($"Entity not found. Type:{typeof(TEntity)}, Id:{id}");

            return entity;
        }

        public virtual TEntity FirstOrDefault(int id) => GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => GetAll().FirstOrDefault(predicate);

        #endregion

        #region Insert

        public abstract void Insert(TEntity entity);

        #endregion

        #region Update

        public abstract void Update(TEntity entity);

        #endregion

        #region Delete

        public abstract void Delete(int id);

        public abstract void Delete(TEntity entity);

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAllList(predicate))
                Delete(entity);
        }

        #endregion

        #region Aggregates

        public virtual int Count() => GetAll().Count();

        public virtual int Count(Expression<Func<TEntity, bool>> predicate) => GetAll().Count(predicate);

        #endregion
    }
}
