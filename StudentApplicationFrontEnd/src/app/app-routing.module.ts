import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListComponent } from './list/list.component';
import { SubjectComponent } from './subject/subject.component';
import { StudentComponent } from './student/student.component';
import { MarkComponent } from './mark/mark.component';
const routes: Routes = [
  { path: 'list', component: ListComponent },
  { path: 'subject', component: SubjectComponent },
  { path: 'student', component: StudentComponent },
  { path: 'mark', component: MarkComponent },
  { path: '**', component: SubjectComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
