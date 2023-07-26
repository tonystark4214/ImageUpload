import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { formatDate } from '@angular/common';
import { gender } from '../gender.enum';
import { UserDataService } from '../Services/user-data.service';


@Component({
  selector: 'app-post-data',
  templateUrl: './post-data.component.html',
  styleUrls: ['./post-data.component.css'],
  
})
export class PostDataComponent implements OnInit {
  constructor(private service: UserDataService,private router:ActivatedRoute,private route:Router) { }
  PostForm: any = FormGroup;
  countryData: any;
  stateData:any;
  ImgLoc:any;
  cityData:any;
  selectedgender: gender | undefined;
  genders = Object.values(gender)
  id:any=0;
  ngOnInit() {

    let builder = new FormBuilder();
    this.PostForm = builder.group({
      firstName: new FormControl("", Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(20)])),
      lastName: new FormControl("", Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(20)])),
      address: new FormControl("", Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(30)])),
      dob: new FormControl("", Validators.compose([Validators.required])),
      phone: new FormControl("", Validators.compose([Validators.required, Validators.min(1000000000), Validators.max(9999999999)])),
      gender: new FormControl("", Validators.required),
      email: new FormControl("", Validators.compose([Validators.required, Validators.email])),
      zipCode: new FormControl("", Validators.compose([Validators.required, Validators.minLength(5), Validators.maxLength(10)])),
      countryId: new FormControl("", Validators.required),
      stateId: new FormControl("", Validators.required),
      cityId: new FormControl("", Validators.required),
      imgLoc:new FormControl("",Validators.required)      
    })
    this.router.queryParams.subscribe(param => {
      this.id = param['id'];
      console.log(this.id)
    });
    this.CountryData();
    if(this.id>0)
      this.getDataById(this.id);
  }

  CountryData() {
    this.service.CountryData().subscribe((res: any) => {
      this.countryData = res.countryData
      console.log(this.countryData);
    })
  }
  StateApi(data: any) {
    console.log(data)
    this.service.StateData(data).subscribe((res:any)=>{
      this.stateData=res.stateData
    })
  }
  CityApi(data: any) {
    console.log(data)
    this.service.CityData(data).subscribe((res:any)=>{
      this.cityData=res.cityData
    })
  }
  convertToByte(data:any){
    var input =data.target;
    var reader = new FileReader();
    reader.onload =()=>{
      console.log(reader.result);
      this.ImgLoc=reader.result;
    }
    reader.readAsDataURL(input.files[0])
  }
  PostUserData(data:any){
    console.log(data)
    // data.country=parseInt(data.country)
    // data.state=parseInt(data.state)
    // data.city=parseInt(data.city)
    data.id=this.id
    data.phone=data.phone.toString();
    data.imgLoc=this.ImgLoc;
    if(data!=null)
      alert("Confirm")
    this.service.PostUserData(data).subscribe((res:any)=>{
      console.log(res);
      if (res.message == "Added Succesfully" || res.message=='Updated Succesfully') {
        this.route.navigate(["showData"]).then(() => {
          window.location.reload();
        });
      }
      else{
        alert("Form Can't Be Null")
      }
    })
  }
  getDataById(id:any){
    this.service.GetUserDataById(id).subscribe((res:any)=>{
    console.log(res)
    this.StateApi(res.data.stateId);
    this.CityApi(res.data.cityId)
    this.PostForm.patchValue(res.data);
    this.PostForm.patchValue({ dob: formatDate(res.data.dob, 'yyyy-MM-dd', 'en') });
    })
  }
  valid(event:any){
    console.log(event)
    if(event.key=='e' || event.key=='E' || event.key=='+' || event.key=='-' || event.key=='.')
      event.preventDefault()
  }

}
