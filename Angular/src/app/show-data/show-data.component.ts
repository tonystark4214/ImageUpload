import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { UserDataService } from '../Services/user-data.service';
@Component({
  selector: 'app-show-data',
  templateUrl: './show-data.component.html',
  styleUrls: ['./show-data.component.css']
})
export class ShowDataComponent implements OnInit {
  constructor(private service: UserDataService, private router: Router) { }
  value: any;
  dataSource = new MatTableDataSource<any>();
  displayedColumns: string[] = ["firstName", "lastName", "phone", "email", "edit", "delete","viewUser"]
  data: any;
  ngOnInit(): void {
    this.UserData();
  }
  UserData() {
    this.service.GetUserData().subscribe((res: any) => {
      this.data = res.listData;
      console.log(this.data);
      this.dataSource = this.data

    })
  }
  Update(id: any) {
    this.router.navigate(['postData'], { queryParams: { id: id } })
  }
  Delete(id: any) {
    this.service.Delete(id).subscribe((res: any) => {
      alert(res.message);

      this.router.navigate(["showData"]).then(() => {
        window.location.reload();
      });
    })
  }
ShowUserData(id:any){
  this.router.navigate(['userData'], { queryParams: { id: id } })
}
}

