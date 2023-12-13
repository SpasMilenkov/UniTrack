import { Component, Input } from '@angular/core';
import { RecommendedMaterial } from '../../models/recommended-material';

@Component({
  selector: 'app-recommended-material-card',
  templateUrl: './recommended-material-card.component.html',
  styleUrls: ['./recommended-material-card.component.scss']
})
export class RecommendedMaterialCardComponent {
  @Input() recommendedMaterial!: RecommendedMaterial;
}
