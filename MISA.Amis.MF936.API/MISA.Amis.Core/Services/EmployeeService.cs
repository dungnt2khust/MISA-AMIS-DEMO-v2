using MISA.Amis.Core.Attributes;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.Repositoties;
using MISA.Amis.Core.Interfaces.Services;
using MISA.Amis.Core.Resources;
using MISA.Amis.Core.Responses;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Fields

        IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;

        #endregion

        #region Constructors

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _serviceResult = new ServiceResult();
        }
        #endregion

        #region Lấy mã nhân viên mới 

        /// <summary>
        /// Lấy mã nv mới
        /// </summary>
        /// <returns>Kết quả nghiệp vụ lấy mã mới</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        public ServiceResult GetNewCode()
        {
            _serviceResult.Data = _employeeRepository.GetNewCode();
            _serviceResult.IsValid = _serviceResult.Data != null;
            return _serviceResult;
        }

        #endregion

        #region Phân trang và lọc dữ liệu nhân viên
        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filterString"></param>
        /// <returns> Kết quả nghiệp vụ phân trang và lọc dữ liệu</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        /// ModifiedBy: NTDUNG (01/09/2021)
        public ServiceResult GetByFilter(int pageSize, int pageNumber, string filterString)
        {
            _serviceResult.Data = _employeeRepository.GetByFilter(pageSize, pageNumber, filterString);
            _serviceResult.IsValid = _serviceResult.Data != null;
            return _serviceResult;
        }

        #endregion

        #region Export dữ liệu
        /// <summary>
        /// Export dữ liệu nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG (27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public MemoryStream Export()
        {
            var employees = _employeeRepository.ExportEmployees();
            var stream = new MemoryStream();
            var properties = employees.First().GetType().GetProperties();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                //Tạo tên bảng
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.SetFromFont(new Font("Arial", 16, FontStyle.Bold));
                workSheet.Row(2).Style.Font.Size = 16;
                workSheet.Cells[1, 1].Value = ResourcesVN.MISA_Employee_Export_Title;

                //Tạo tiêu đề cho bảng chứa thông tin nhân viên (ở hàng 3)
                workSheet.Cells[3, 1].Value = "STT";
                int headerIndex = StyleHeader(properties, workSheet);
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
                        var propMisaExport = property.GetCustomAttributes(typeof(MISAExport), true);
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
    

        /// <summary>
        /// Style header
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="workSheet"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG (08/09/2021)
        private static int StyleHeader(System.Reflection.PropertyInfo[] properties, ExcelWorksheet workSheet)
        {
            var headerIndex = 2;
            foreach (var property in properties)
            {
                // Lọc ra những trường muốn Export
                var propMisaExport = property.GetCustomAttributes(typeof(MISAExport), true);
                if (propMisaExport.Length > 0)
                {
                    var fieldName = (propMisaExport[0] as MISAExport).FieldName;
                    workSheet.Cells[3, headerIndex].Value = fieldName;
                    headerIndex++;
                }
            }
            workSheet.Cells[3, 1, 3, headerIndex - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[3, 1, 3, headerIndex - 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            workSheet.Cells[1, 1, 1, headerIndex - 1].Merge = true;
            workSheet.Cells[2, 1, 2, headerIndex - 1].Merge = true;
            return headerIndex;
        }

        /// <summary>
        /// định dạng ngày/tháng/năm
        /// </summary>
        /// <param name="dateInput">datetime cần định dạng</param>
        /// <returns>String - ngày đã được định dạng</returns>
        /// CreatedBy: NTDUNG (01/09/2021)
        public string formatDate(object dateInput)
        {
            if (dateInput != null)
            {
                DateTime date = DateTime.Parse(dateInput.ToString(), null);
                return date.ToString("dd/MM/yyyy", null);
            }
            return null;

        }
        #endregion
    }
}
