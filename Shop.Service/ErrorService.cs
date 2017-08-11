﻿using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
	public interface IErrorService
	{
		Error Create(Error error);
		void Save();
	}
	public class ErrorService : IErrorService
	{
		IErrorRepository _errorRepository;
		IUnitOfWork _unitOfWork;

		public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
		{
			this._errorRepository = errorRepository;
			this._unitOfWork = unitOfWork;
		}

		public Error Create(Error error)
		{
			return this._errorRepository.Add(error);
		}

		public void Save()
		{
			this._unitOfWork.Commit();
		}
	}
}
