import { Component } from '@angular/core';
import { SupportComponent } from "../../shared/support/support.component";
import { WorkComponent } from "../../shared/work/work.component";
import { TestimonialComponent } from "../../shared/testimonial/testimonial.component";
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-about',
  imports: [CommonModule,RouterModule],
  templateUrl: './about.component.html',
  styleUrl: './about.component.css'
})
export class AboutComponent {

}
