﻿using BusinessLayer.Models.Director;

namespace BusinessLayer.Services.Contracts
{
	public interface IDirectorService
	{
		DirectorDTO Create(DirectorCreateDTO director);

		DirectorDTO Update(int directorId, DirectorUpdateDTO director);

		void Delete(int directorId);

		DirectorDTO GetById(int directorId);

		IEnumerable<DirectorDTO> GetAll();
	}
}
