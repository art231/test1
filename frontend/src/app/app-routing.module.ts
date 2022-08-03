import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddTutorialComponent } from './components/add-tutorial/add-tutorial.component';
import { TutorialUpdateComponent } from './components/tutorial-update/tutorial-update.component';
import { TutorialsListComponent } from './components/tutorials-list/tutorials-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'tutorials', pathMatch: 'full' },
  { path: 'add', component: AddTutorialComponent },
  { path: 'tutorials/:id', component: TutorialUpdateComponent },
  { path: 'tutorials', component: TutorialsListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }