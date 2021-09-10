import { Component, OnInit } from '@angular/core';
import { data } from 'jquery';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private service: SharedService) { }
  EmployeeList : any = [];
  ngOnInit(): void {
    this.refreshEmpList();
  }


  refreshEmpList(){
    this.service.getEmpList().subscribe(
      data=>(this.EmployeeList=data)

    );
  }

  editClick(){

  }

  deleteClick(mustafa : any){
    this.service.deleteEmployee(mustafa.employeeID).subscribe(data=>{
      alert(data.toString());
      this.refreshEmpList();
    })
  }
  


}
