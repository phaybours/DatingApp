import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';

import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';


@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });

    this.galleryOptions = [{
      width: '500px',
      height: '500px',
      imagePercent: 100,
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview: false
    }];
    this.galleryImages = this.loadImages();
  }

  loadImages() {
    const imageUrls = [];
    for (const i of this.user.photos){
      imageUrls.push({
        small: i.url,
        medium: i.url,
        big: i.url,
        description: i.description
      });
    }

    // tslint:disable-next-line: prefer-for-of
    // for (let i = 0; i < this.user.photos.length; i++) {
    //   imageUrls.push({
    //     small: this.user.photos[i].url,
    //     medium: this.user.photos[i].url,
    //     big: this.user.photos[i].url
    //   });
    // }
    return imageUrls;
  }

}
