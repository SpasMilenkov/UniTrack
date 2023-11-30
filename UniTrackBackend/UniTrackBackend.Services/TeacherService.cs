﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data;

namespace UniTrackBackend.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly UnitOfWork _unitOfWork;

        public TeacherService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _unitOfWork.TeacherRepository.GetAllAsync();
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {

            return await _unitOfWork.TeacherRepository.GetByIdAsync(id);
        }

        public async Task<Teacher> AddTeacherAsync(Teacher teacher)
        {
            await _unitOfWork.TeacherRepository.AddAsync(teacher);
            await _unitOfWork.SaveAsync();
            return teacher;
        }

        public async Task<Teacher> UpdateTeacherAsync(Teacher teacher)
        {
            await _unitOfWork.TeacherRepository.UpdateAsync(teacher);
            await _unitOfWork.SaveAsync();
            return teacher;
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(id);
            if (teacher != null)
            {
                await _unitOfWork.TeacherRepository.DeleteAsync(teacher.Id);
                await _unitOfWork.SaveAsync();
            }
        }
    }

}
