using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.ApplicationCore
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository):base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public ServiceResult GetEmployeeFilterPaging(string employeeFilter, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _employeeRepository.GetEmployeeFilterPaging(employeeFilter, pageIndex, pageSize);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public ServiceResult GetNewEmployeeCode()
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _employeeRepository.GetNewEmployeeCode();
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public ServiceResult CheckEmployeeCodeExist(string employeeCode)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _employeeRepository.CheckEmployeeCodeExist(employeeCode);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        /// <summary>
        /// Export dữ liệu nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public MemoryStream Export()
        {
            try
            {
                var employees = _employeeRepository.ExportEmployee();
                var stream = new MemoryStream();
                var properties = employees.First().GetType().GetProperties();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                    //Tạo tên bảng
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(1).Style.Font.SetFromFont(new Font("Arial", 16, FontStyle.Bold));
                    workSheet.Row(2).Style.Font.Size = 16;
                    workSheet.Cells[1, 1].Value = Resources.ResourceVN.Employee_Export_Title;

                    //Tạo tiêu đề cho bảng chứa thông tin nhân viên (ở hàng 3)
                    workSheet.Cells[3, 1].Value = "STT";
                    var headerIndex = 2;
                    foreach (var property in properties)
                    {
                        var propMisaExport = property.GetCustomAttributes(typeof(MisaExport), true);
                        if (propMisaExport.Length > 0)
                        {
                            var fieldName = (propMisaExport[0] as MisaExport).FieldName;
                            workSheet.Cells[3, headerIndex].Value = fieldName;
                            headerIndex++;
                        }
                    }
                    workSheet.Cells[3, 1, 3, headerIndex - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[3, 1, 3, headerIndex - 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    workSheet.Cells[1, 1, 1, headerIndex - 1].Merge = true;
                    workSheet.Cells[2, 1, 2, headerIndex - 1].Merge = true;
                    //Căn giữa và in đậm cho các tiêu đề
                    workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(3).Style.Font.SetFromFont(new Font("Arial", 10, FontStyle.Bold));
                    //Đổ thông tin nhân viên lên các hàng của bảng
                    int recordIndex = 4;
                    foreach (var employee in employees)
                    {
                        var propertiesBody = employee.GetType().GetProperties();
                        workSheet.Cells[recordIndex, 1].Value = (recordIndex - 3).ToString();
                        var columnIndex = 2;
                        foreach (var property in propertiesBody)
                        {
                            var propMisaExport = property.GetCustomAttributes(typeof(MisaExport), true);
                            if (propMisaExport.Length > 0)
                            {
                                if (property.Name == "DateOfBirth")
                                {
                                    //căn giữa
                                    workSheet.Cells[recordIndex, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    //định dạng ngày/tháng/năm
                                    workSheet.Cells[recordIndex, columnIndex].Value = formatDate(property.GetValue(employee));
                                }
                                else
                                {
                                    workSheet.Cells[recordIndex, columnIndex].Value = property.GetValue(employee);
                                }
                                columnIndex++;
                            }
                        }
                        recordIndex++;
                    }
                    //Auto fit
                    workSheet.Cells.AutoFitColumns();
                    //đặt font cho nội dung bảng
                    var allCellsTableBody = workSheet.Cells[4, 1, recordIndex - 1, headerIndex - 1];
                    allCellsTableBody.Style.Font.SetFromFont(new Font("Times New Roman", 11));
                    //Thêm border
                    var allCellsTable = workSheet.Cells[3, 1, recordIndex - 1, headerIndex - 1];
                    allCellsTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    allCellsTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    allCellsTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    allCellsTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //save
                    package.Save();
                }
                stream.Position = 0;
                return stream;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        /// <summary>
        /// định dạng ngày/tháng/năm
        /// </summary>
        /// <param name="dateInput">datetime cần định dạng</param>
        /// <returns></returns>
        /// author: NTDUNG(
        public string formatDate(object dateInput)
        {
            if(dateInput != null)
            {
                DateTime date = DateTime.Parse(dateInput.ToString(), null);
                return date.ToString("dd/MM/yyyy", null);
            }
            return null;
           
        }
        #endregion
    }
}
