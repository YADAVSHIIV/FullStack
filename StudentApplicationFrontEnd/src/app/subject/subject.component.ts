import { Component, OnInit } from '@angular/core';
import { Subject } from '../_interfaces/subject';
import { SubjectService } from '../_services/subject.service';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.scss'],
})
export class SubjectComponent implements OnInit {
  public subject: Subject = {};
  list: Subject[] = [];
  constructor(private subjectservice: SubjectService) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.subjectservice.getAll().subscribe((data) => {
      this.list = data;
    });
  }

  delete(id: number): void {
    this.subjectservice.delete(id).subscribe((data) => {
      this.list.forEach((item, index) => {
        if (item.id === data.id) {
          this.list.splice(index, 1);
        }
      });
    });
  }

  add(rec: Subject): void {
    this.subjectservice.add(rec).subscribe((data) => {
      this.list.unshift(data);
    });
  }
}
