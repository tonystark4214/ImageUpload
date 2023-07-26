import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserdataComponent } from './userdata/userdata.component';
import { PostDataComponent } from './post-data/post-data.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { ShowDataComponent } from './show-data/show-data.component';
const routes: Routes = [
  {
    path:'userData',
    component:UserdataComponent
  }
  ,{
    path:'postData',
    component:PostDataComponent
  },
  {
    path:'searchUser',
    component:LandingPageComponent
  },
  {
    path:'showData',
    component:ShowDataComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule {

 }
