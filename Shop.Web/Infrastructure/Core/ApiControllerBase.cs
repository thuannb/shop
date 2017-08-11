using Shop.Model.Models;
using Shop.Service;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Infrastructure.Core
{
	public class ApiControllerBase : ApiController
	{
		private IErrorService _errorService;

		public ApiControllerBase(IErrorService errorService)
		{
			this._errorService = errorService;
		}

		protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
		{
			HttpResponseMessage responseMessage = null;

			try
			{
				responseMessage = function.Invoke();
			}
			catch (DbEntityValidationException ex)
			{
				foreach (var eve in ex.EntityValidationErrors)
				{
					Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
					foreach (var ve in eve.ValidationErrors)
					{
						Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
					}
				}
				LogError(ex);
				responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
			}
			catch (DbUpdateException dbEx)
			{
				LogError(dbEx);
				//InnerException:Show lỗi trong cơ sở dữ liệu
				responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
			}
			catch (Exception ex)
			{
				LogError(ex);
				responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
				throw;
			}

			return responseMessage;
		}

		private void LogError(Exception ex)
		{
			try
			{
				Error error = new Error();
				error.Message = ex.Message;
				error.StackTrace = ex.StackTrace;
				error.CreateDate = DateTime.Now;

				_errorService.Create(error);
				_errorService.Save();
			}
			catch
			{
			}
		}
	}
}