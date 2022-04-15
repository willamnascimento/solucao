import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { PeoplePageComponent } from './containers/people-page/people-page.component';


const routes: Routes = [
  {
    path: '',
    component: PeoplePageComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class PeopleRoutingModule {
}
