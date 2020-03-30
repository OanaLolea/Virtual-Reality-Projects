#include <cmath>
#include <iostream>
#include <string>

#include "Vector.h"
#include "Line.h"
#include "Geometry.h"
#include "Sphere.h"
#include "Image.h"
#include "Color.h"
#include "Intersection.h"
#include "Material.h"

#include "Scene.h"

using namespace std;
using namespace rt;

float imageToViewPlane(int n, int imgSize, float viewPlaneSize) {
    float u = (float)n*viewPlaneSize / (float)imgSize;
    u -= viewPlaneSize / 2;
    return u;
}

const Intersection findFirstIntersection(const Line& ray,
    float minDist, float maxDist) {
    Intersection intersection;

    for (int i = 0; i < geometryCount; i++) {
        Intersection in = scene[i]->getIntersection(ray, minDist, maxDist);
        if (in.valid()) {
            if (!intersection.valid()) {
                intersection = in;
            }
            else if (in.t() < intersection.t()) {
                intersection = in;
            }
        }
    }

    return intersection;
}

int main() {
    Vector viewPoint(-50, 10, 0);
    Vector viewDirection(50, -10, 100);
    Vector viewUp(0, -1, 0);

	viewDirection.normalize();
	viewUp.normalize();

    float frontPlaneDist = 0;
    float backPlaneDist = 1000;

    float viewPlaneDist = 65;
    float viewPlaneWidth = 160;
    float viewPlaneHeight = 120;

    int imageWidth = 1024;
    int imageHeight = 768;

    Vector viewParallel = viewUp^viewDirection;
    viewParallel.normalize();

    Image image(imageWidth, imageHeight);





	for(float i =0;i< imageWidth; i++)
		for (float j = 0; j < imageHeight; j++) {


			float width = imageToViewPlane(i, imageWidth, viewPlaneWidth);
			float height = imageToViewPlane(j, imageHeight, viewPlaneHeight);

			Vector onPlaneVectorHeight = viewUp * height;
			Vector onPlaneVectorWidth = viewParallel * width;

			Vector view = viewDirection * viewPlaneDist;

			Vector point = viewPoint + view + onPlaneVectorHeight + onPlaneVectorWidth;

			Line line = Line(viewPoint, point, false);

			Intersection intersection = findFirstIntersection(line, frontPlaneDist, backPlaneDist);

			if (intersection.valid()) {

				Color c = intersection.geometry()->color();

				for (int k = 0; k < lightCount;k++) {
					Vector V = intersection.vec();
					
					Vector N = intersection.geometry()->normal(V);
					N.normalize();


					Vector L = lights[k]->position();
					Vector T = L - V;
					T.normalize();


					
					c += lights[k]->ambient() * intersection.geometry()->material().ambient();


					if (N * T>0)
						c += intersection.geometry()->material().diffuse() * lights[k]->diffuse() * (N * T);
						
				
						

					Vector C = viewPoint;

					Vector E = C - V;
					E.normalize();


					Vector R = N * (N * T) * 2 - T;
					R.normalize();


					if (E * R>0)
						c += intersection.geometry()->material().specular() * lights[k]->specular() * pow(E * R, intersection.geometry()->material().shininess());
						
				
					
					c *= lights[k]->intensity();


					image.setPixel(i, j, c);
				}
			}
			else
				image.setPixel(i, j, Color{ 0.0,0.0,0.0});
			

		}

    image.store("scene.png");

    for (int i = 0; i < geometryCount; i++) {
        delete scene[i];
    }

	for (int i = 0; i < lightCount; i++) {
		delete lights[i];
	}

    return 0;
}
