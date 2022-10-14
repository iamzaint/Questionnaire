using System;
using System.Collections.Generic;
using System.Linq;

namespace StealthLibrary.DBModel
{
    public partial class tbl_Questionnaire
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }


        public List<tbl_Questionnaire> getAllQuestionnairedata()
        {
            try
            {
                using (var context = new StealthBaseEntities())
                {
                     var list=context.tbl_Questionnaire.Where(X => X.Inactive == false && X.Deleted==false && X.IsParent ==true).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<tbl_Questionnaire> GetAllSUBQuestionnairedata(string QuestionID)
        {
            try
            {
                using (var context = new StealthBaseEntities())
                {
                    return context.tbl_Questionnaire.Where(X => X.Inactive == false && X.Deleted == false && X.IsParent == false && X.IsChild == true && X.ParentID == QuestionID).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<tbl_QuestionnaireCategory> LoadQuestionCategory()
        {
            try
            {
                using (var context = new StealthBaseEntities())
                {

                    return context.tbl_QuestionnaireCategory.Where(X => X.inactive == false).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool AddData(tbl_Questionnaire obj)
        {
            try
            {
                using (var context = new StealthBaseEntities())
                {
                    obj.QuestionCategory = obj.QuestionCategory;
                    obj.IsParent = obj.IsParent;
                    if(obj.IsParent == true && obj.IsParent != null)
                    {
                        obj.IsChild = false;
                        obj.ParentID = "0";
                    }
                    else
                    {
                        obj.IsChild = true;
                        obj.ParentID = obj.ParentID;
                        if(obj.ParentID!="")
                        {
                            int PID = Convert.ToInt32(obj.ParentID);
                            var LinkToParent = context.tbl_Questionnaire.Where(x => x.QuestionID == PID).FirstOrDefault();
                            LinkToParent.SubQuestion = true;
                            context.SaveChanges();
                        }

                    }


                    obj.Question = obj.Question;
                    obj.Answer = obj.Answer;

                    context.tbl_Questionnaire.Add(obj);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public tbl_Questionnaire getAlldataByID(int id)
        {
            try
            {
                using (var context = new StealthBaseEntities())
                {
                    return context.tbl_Questionnaire.SingleOrDefault(x => x.QuestionID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool DeleteData(int id)
        {
            try
            {
                using (var context = new StealthBaseEntities())
                {
                    var result = context.tbl_Questionnaire.SingleOrDefault(x => x.QuestionID == id);
                    if (result != null)
                    {
                        result.Deleted = true;
                        context.SaveChanges();


                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
