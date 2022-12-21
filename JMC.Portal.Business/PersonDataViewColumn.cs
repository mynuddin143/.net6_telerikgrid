using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business
{
    public partial class PersonDataViewColumn
    {
        public static IQueryable<PersonDataViewColumn> FindByPersonID(PortalEntities db, long userId)
        {
            var query = (from x in db.PersonDataViewColumns where x.PersonID == userId select x);
            return query.Any() ? query : null;
        }

        public static IQueryable<PersonDataViewColumn> FindByDataViewIDAndPersonID(PortalEntities db, long dataViewID, long userID)
        {
            var query = (from x in db.PersonDataViewColumns where x.DataViewID == dataViewID && x.PersonID == userID select x);
            return query.Any() ? query : null;
        }

        public static IEnumerable<PersonDataViewColumn> GetPersonDataViewColumns(PortalEntities db, Enums.DataViews dataViewEnum, long userID)
        {
            var personDataViewColumns = PersonDataViewColumn.FindByDataViewIDAndPersonID(db, (long)dataViewEnum, userID);
            if (personDataViewColumns != null) 
                return personDataViewColumns.OrderBy(x => x.Order);

            var dataViews = (from x in db.DataViews where x.DataViewID == (long)dataViewEnum select x).SingleOrDefault();
            var columns = new List<PersonDataViewColumn>();
            foreach (var x in dataViews.DataViewColumns.OrderBy(x => x.Order))
            {
                columns.Add(new PersonDataViewColumn
                {
                    DataViewColumnID = x.DataViewColumnID,
                    DataViewID = x.DataViewID,
                    PersonID = userID,
                    FieldName = x.FieldName,
                    DisplayFieldName = x.DisplayFieldName,
                    Width = x.Width,
                    Title = x.Title,
                    GroupFooterTemplate = x.GroupFooterTemplate,
                    FooterTemplate = x.FooterTemplate,
                    GroupHeaderTemplate = x.GroupHeaderTemplate,
                    HeaderTemplate = x.HeaderTemplate,
                    HtmlAttributeClass = x.HtmlAttributeClass,
                    HtmlAttributeStyle = x.HtmlAttributeStyle,
                    Format = x.Format,
                    SortDirection = x.SortDirection,
                    GroupByOrder = x.GroupByOrder,
                    GroupByDirection = x.GroupByDirection,
                    Encoded = x.Encoded,
                    Sortable = x.Sortable,
                    Filterable = x.Filterable,
                    Groupable = x.Groupable,
                    Order = x.Order,
                    Visible = x.Visible,
                    IncludeInMenu = x.IncludeInMenu
                });
            }
            return columns;
        }

        public static void PopulateFromDatabase(PortalEntities db, IEnumerable<PersonDataViewColumn> columns)
        {
            var colCount = columns.Count();
            foreach (var col in columns.OrderBy(x => x.Order))
            {
                var dbCols = (from x in db.DataViewColumns where x.DataViewColumnID == col.DataViewColumnID select x);
                var dbColCount = dbCols.Count();

                var dbCol = dbCols.Single();
                col.FieldName = dbCol.FieldName;
                col.DisplayFieldName = dbCol.DisplayFieldName;
                col.Title = dbCol.Title;
                col.GroupFooterTemplate = dbCol.GroupFooterTemplate;
                col.FooterTemplate = dbCol.FooterTemplate;
                col.GroupHeaderTemplate = dbCol.GroupHeaderTemplate;
                col.HeaderTemplate = dbCol.HeaderTemplate;
                col.HtmlAttributeClass = dbCol.HtmlAttributeClass;
                col.HtmlAttributeStyle = dbCol.HtmlAttributeStyle;
                col.Format = dbCol.Format;
                col.Encoded = dbCol.Encoded;
                col.Sortable = dbCol.Sortable;
                col.Filterable = dbCol.Filterable;
                col.Groupable = dbCol.Groupable;
                col.Printable = dbCol.Printable;
                col.Order = col.Order;
                col.IncludeInMenu = dbCol.IncludeInMenu;
            }
        }

        public static PersonDataViewColumn GetPersonalDataViewColumnByUserAndFieldName(PortalEntities db, Enums.DataViews dataViewEnum, long userID, string fieldName)
        {
            return (from x in GetPersonDataViewColumns(db, dataViewEnum, userID) where x.FieldName == fieldName select x).Single();
        }

        public static IEnumerable<PersonDataViewColumn> RemoveNonPrintable(IEnumerable<PersonDataViewColumn> columns)
        {
            return (from x in columns where x.Printable select x).ToArray();
        }

    }
}
