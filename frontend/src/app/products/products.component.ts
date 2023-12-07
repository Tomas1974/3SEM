import { Component, OnInit } from "@angular/core";
import {ProductsService, Avatar} from './products.service';
import {CartService} from '../cart/cart.service';
import {Router} from "@angular/router";
import {ModalController} from "@ionic/angular";
import {createAvatarComponent} from "./createAvatarComponent";

@Component({
  template: `
    <app-title title="AVATARS"></app-title>
    <ion-content>
      <ion-grid [fixed]="true">
        <ion-row>
          <ion-col *ngFor="let avatar of avatar$; index as i">
            <ion-card>
              <ion-card-header>
                <ion-card-title class="name">{{avatar.avatar_name}}</ion-card-title>
                <ion-card-title class="price">{{avatar.avatar_price}} € </ion-card-title>
              </ion-card-header>
              <img src="https://robohash.org/{{avatar.avatar_name}}.png" height="150px" width="150px"/>
              <ion-card-content>
                <ion-button class="button" (click)="saveData(avatar)" fill="clear" >
                  <ion-icon name="cart-outline"></ion-icon>
                </ion-button>
              </ion-card-content>
              <ion-button class="button" (click)="updateAvatar(avatar)">Update</ion-button>
            </ion-card>
          </ion-col>
        </ion-row>
        <ion-button class="button" (click)="createAvatar()">Create</ion-button>
      </ion-grid>
    </ion-content>
  `
})

export class ProductsComponent implements OnInit {


  avatar$?: Avatar[];
  cartArray: Avatar[];


  constructor(private productService: ProductsService, readonly router: Router, public modalController: ModalController) {

    this.cartArray = [];

}

  saveData(avatar: Avatar){

    this.cartArray.push(avatar);
    sessionStorage.setItem("cart", JSON.stringify(this.cartArray))

  }

  ngOnInit(): void {

    this.productService.getAllProducts().subscribe(result => {
      this.avatar$ = result.responseData;
    })
  }

  async updateAvatar(avatar: Avatar){

  }


  async createAvatar(){
    const model = await this.modalController.create({
      component: createAvatarComponent
    })
    model.present();
  }
}
