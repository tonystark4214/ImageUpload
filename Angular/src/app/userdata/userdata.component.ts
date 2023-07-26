import { Component, OnInit } from '@angular/core';
import { UserDataService } from '../Services/user-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
@Component({
  selector: 'app-userdata',
  templateUrl: './userdata.component.html',
  styleUrls: ['./userdata.component.css']
})
export class UserdataComponent implements OnInit {
  constructor(private service: UserDataService, private router: ActivatedRoute,private route:Router) { }
  Id: any;
  data:any=[]
  imgUrl:any

  
  
  ngOnInit(): void {
    this.router.queryParams.subscribe(param => {
      this.Id = param['id'];
    });
    this.UserData();
    
  }
  UserData() {
    this.service.GetUserDataById(this.Id).subscribe((res: any) => {
      this.data=res.data;
     
      if(res.message=="User Doesn't Exist"){
        alert("User Doesn't Exist")
        this.route.navigate(['/showData'])
      }
    
      this.imgUrl = this.data.imgLoc;
    })
  }
}
