import { Component, OnInit } from '@angular/core';

import { Student } from '../_interfaces/student';
import { StudentService } from '../_services/student.service';
import { QueryService } from '../_services/query.service';
import { MarkService } from '../_services/mark.service';
import { Mark } from '../_interfaces/mark';
import { NotificationService } from '../_services/notification.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.scss'],
})
export class StudentComponent implements OnInit {
  public student: Student = {};
  list: Student[] = [];
  pageNo = 1;
  totalRec = 0;
  pageSize = 50;
  isEdit = false;

  constructor(
    private studentservice: StudentService,
    private queryservice: QueryService,
    private markservice: MarkService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.loadAll();
  }
  edit(std: Student): void {
    this.student.firstName = std.firstName;
    this.student.lastName = std.lastName;
    this.student.class = std.class;
    this.student.id = std.id;
    this.isEdit = true;
  }

  cancleEdit(): void {
    this.student.firstName = '';
    this.student.lastName = '';
    this.student.class = 0;
    this.student.id = 0;
    this.isEdit = false;
  }

  loadAll(): void {
    this.studentservice
      .list('', this.pageNo, this.pageSize)
      .subscribe((data) => {
        this.list = data.records;
        this.pageNo = data.pageNo;
        this.totalRec = data.total;
      });
  }

  deleteStudent(id: number): void {
    this.studentservice.delete(id).subscribe((data) => {
      this.list.forEach((item, index) => {
        if (item.id === data.id) {
          this.list.splice(index, 1);
        }
      });

      this.notificationService.showSuccess('success');
    });
  }

  deleteMark(student: Student, id: number): void {
    this.markservice.delete(id).subscribe((data) => {
      // student.marks?.forEach((item, index) => {
      //   if (item.ID === data.ID) {
      //     student.marks.splice(index, 1);
      //   }
      // });
    });
  }

  addStudent(rec: Student): void {
    this.studentservice.add(rec).subscribe((data) => {
      this.list.unshift(data);
      this.notificationService.showSuccess('success');
    });
  }
  addMark(student: Student, rec: Mark): void {
    this.markservice.add(rec).subscribe((data) => {
      // student.marks.unshift(data);
    });
  }
  updateStudent(rec: Student): void {
    this.studentservice.update(rec).subscribe((data) => {
      this.list.forEach((item, index) => {
        if (item.id === data.id) {
          item.firstName = data.firstName;
          item.lastName = data.lastName;
          item.class = data.class;
        }
      });
      this.cancleEdit();
      this.notificationService.showSuccess('success');
    });
  }
  updateMark(student: Student, rec: Mark): void {
    this.markservice.update(rec).subscribe((data) => {
      // student.marks.unshift(data);
    });
  }
}
