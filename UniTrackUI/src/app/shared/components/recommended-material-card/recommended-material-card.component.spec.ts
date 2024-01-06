import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecommendedMaterialCardComponent } from './recommended-material-card.component';

describe('RecommendedMaterialCardComponent', () => {
  let component: RecommendedMaterialCardComponent;
  let fixture: ComponentFixture<RecommendedMaterialCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecommendedMaterialCardComponent]
    });
    fixture = TestBed.createComponent(RecommendedMaterialCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
