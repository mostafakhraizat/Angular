import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {
  DepartmentList : any = [];


  constructor(public service : SharedService) { }

  ngOnInit(): void {
    this.RefreshDepList();
  }

  ModalTitle:string = "";
  ActivateAddEditDepComp:boolean=false;
  dep:any;


  addClick(){
    this.dep={
      DepartmentId:0,
      DepartmentName:""
    }
    this.ModalTitle="Add new Department";
    this.ActivateAddEditDepComp=true;
  }

  closeClick(){
    this.ActivateAddEditDepComp = false
    this.RefreshDepList();
  }


  editClick(mustafa:any){
    this.ModalTitle = "Edit Department";
    alert(mustafa.departmentID)
  }

  deleteClick(mustafa : any){
    this.service.deleteDepartment(mustafa.departmentID).subscribe(data=>{
      alert(data.toString());
      this.RefreshDepList();
    })
  }



  RefreshDepList(){
    this.service.getDepList().subscribe(
      data=>{this.DepartmentList = data}
    );
  }
}
