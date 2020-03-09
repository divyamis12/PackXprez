import { Component, OnInit } from '@angular/core';
import { PackxprezServiceService } from '../PackXpreZ-Service/packxprez-service.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { IAddress } from '../PackXpreZ-interfaces/address';
import { HostListener, ElementRef } from '@angular/core';
@Component({
  selector: 'app-add-addess',
  templateUrl: './add-addess.component.html',
  styleUrls: ['./add-addess.component.css']
})
export class AddAddessComponent implements OnInit {
  emailid: string;
  Building: string;
  Street: string;
  Locality: string;
  Pincode: number;
  topPosToStartShowing: number = 100;
  isShow: boolean;
  addresses: IAddress[];
  constructor(private service: PackxprezServiceService, private router: Router) { }

  ngOnInit() {
    this.emailid = sessionStorage.getItem('emailid');
    if (this.emailid != null) {
     
    }
    else {
      this.router.navigate(['Customerlogin']);
    }
    this.service.getAllUserAddress(this.emailid).subscribe(
      responseSuccess => {
        this.addresses = responseSuccess;
        console.log(this.addresses);

      },
      reponseError => {
        this.addresses = null;
      },
      () => { }
    );

  }

  update(form: NgForm) {
    var addr: IAddress;
    addr = {
      addressId: 0,
      emailid: this.emailid,
      buildingNo: form.value.Building,
      streetName: form.value.Street,
      locality: form.value.Locality,
      pincode: form.value.Pincode
    }
    this.service.addUserAddress(addr).subscribe(
      resSuccess => {
        console.log(resSuccess);
        if (resSuccess) {
          alert("Address Added Successfully");
          this.router.navigate(['home'])
        }
        else {
          alert("Could not add Address")
        }
      },
      resError => { console.log(resError);},
      () => {}
    );
  }

  remove(addr: number) {
    if (this.addresses.length == 1) {
      alert("1 address should be there!");
    }
    else {
      if (addr == 0) {
        alert("Select Address to Remove");

      }
      else {
        this.service.removeadd(addr).subscribe(
          resSuccess => {
            console.log(resSuccess);
            if (resSuccess) {
              alert("Address Removed Successfully");
              this.router.navigate(['home'])
            }
            else {
              alert("Could not removed Address")
            }
          },
          resError => { console.log(resError); },
          () => { }
        );
      }
    }
  }
  cancel() {
    this.router.navigate(['home']);
  }

  checkScroll() {

    // window의 scroll top
    // Both window.pageYOffset and document.documentElement.scrollTop returns the same result in all the cases. window.pageYOffset is not supported below IE 9.

    const scrollPosition = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;

    console.log('[scroll]', scrollPosition);

    if (scrollPosition >= this.topPosToStartShowing) {
      this.isShow = true;
    } else {
      this.isShow = false;
    }
  }

  // TODO: Cross browsing
  gotoTop() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }
}

