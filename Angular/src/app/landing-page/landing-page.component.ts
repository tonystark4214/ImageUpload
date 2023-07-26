import { Component } from '@angular/core';
import { UserDataService } from '../Services/user-data.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {
  constructor(private service:UserDataService, private router:Router){}
  SearchForm: any = FormGroup
  ngOnInit(): void {

    let builder = new FormBuilder();
    this.SearchForm = builder.group({
      Search:new FormControl("",Validators.required)
    })
  }
  Search(data:any){
    this.router.navigate(['userData'], { queryParams: {id: data.Search}})
  }
}
