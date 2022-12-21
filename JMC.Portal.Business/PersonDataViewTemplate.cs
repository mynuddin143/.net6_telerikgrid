using System.Collections.Generic;
using System.Linq;

namespace JMC.Portal.Business {
    public partial class PersonDataViewTemplate {

        public static IQueryable<PersonDataViewTemplate> FindByPersonID(PortalEntities db, long userId) {
            var query = (from x in db.PersonDataViewTemplates where x.PersonID == userId select x);
            return query.Any() ? query : null;
        }

        public static PersonDataViewTemplate InsertOrUpdate(PortalEntities db, User user, long dataViewID, IEnumerable<PersonDataViewColumnTemplate> columns) {

          var query = (from x in db.PersonDataViewTemplates where x.DataViewID == dataViewID && x.PersonID == user.UserID select x);

          var personDataViewTemplate = new PersonDataViewTemplate();
          if (query.Any()) {
              personDataViewTemplate = query.Single();
          } else {
              personDataViewTemplate.DataViewID = dataViewID;
              personDataViewTemplate.PersonID = user.UserID;
              db.PersonDataViewTemplates.Add(personDataViewTemplate);
          }

					var populatedColumns = AddMissingColumns(db, dataViewID, columns);

	        foreach (var column in populatedColumns) {
              var columnQuery = (from x in personDataViewTemplate.PersonDataViewColumnTemplates where x.DataViewColumnID == column.DataViewColumnID select x);
              PersonDataViewColumnTemplate personDataViewColumnTemplate;
              if (columnQuery.Any()) {
                  personDataViewColumnTemplate = columnQuery.Single();
              } else {
                  personDataViewColumnTemplate = new PersonDataViewColumnTemplate();
                  personDataViewTemplate.PersonDataViewColumnTemplates.Add(personDataViewColumnTemplate);
              }

              personDataViewColumnTemplate.DataViewColumnID = column.DataViewColumnID;
              personDataViewColumnTemplate.Width = column.Width;
              personDataViewColumnTemplate.Visible = column.Visible;
              personDataViewColumnTemplate.Order = column.Order;
              personDataViewColumnTemplate.SortDirection = column.SortDirection;
              personDataViewColumnTemplate.GroupByOrder = column.GroupByOrder;
							personDataViewColumnTemplate.GroupByDirection = column.GroupByDirection;
              personDataViewColumnTemplate.FirstFilterOperator = column.FirstFilterOperator;
              personDataViewColumnTemplate.FirstFilterValue = column.FirstFilterValue;
              personDataViewColumnTemplate.Logic = column.Logic;
              personDataViewColumnTemplate.SecondFilterOperator = column.SecondFilterOperator;
              personDataViewColumnTemplate.SecondFilterValue = column.SecondFilterValue;
          }

          return personDataViewTemplate;
        }

			// Include any missing columns that were removed due to permissions.
			// These should still be in the personDataViewColumnTemplate incase their permissions change
			// It should always be a one-to-one mappting with DataViewColumn
	    private static List<PersonDataViewColumnTemplate> AddMissingColumns(PortalEntities db, long dataViewID, IEnumerable<PersonDataViewColumnTemplate> columns){
		    var columnIDs = (from x in columns select x.DataViewColumnID).ToList();
				
		    var missingOriginalColumns = (from x in db.DataViewColumns where x.DataViewID == dataViewID && !columnIDs.Contains(x.DataViewColumnID) select x).ToList();

		    var missingPersonColumns = (from x in missingOriginalColumns
			    select new PersonDataViewColumnTemplate{
				    PersonDataViewColumnTemplateID = 0,
				    PersonDataViewTemplateID = columns.First().PersonDataViewTemplateID,
				    DataViewColumnID = x.DataViewColumnID,
				    Width = x.Width,
				    Visible = true,
				    Order = x.Order,
			    }).ToList();

		    var populatedColumns = columns.ToList();
		    populatedColumns.InsertRange(0, missingPersonColumns);
		    return populatedColumns;
	    }
    }
}