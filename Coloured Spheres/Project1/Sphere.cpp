#include "Sphere.h"
#include <algorithm>

using namespace rt;

Intersection Sphere::getIntersection(const Line& line, float minDist, float maxDist) {
	
	auto R = this->radius();


	const auto A = line.dx() * line.dx();

	const auto B = 2 * (line.dx() * (line.x0() - this->center()));

	const auto C = (line.x0()-this->center()) * (line.x0()-this->center()) - R*R;

	const auto delta = B * B - 4 * A * C;

	if (delta < 0) {

		return Intersection(false, this, &line, 0);
	}
	else
		if (delta == 0) {
			const auto t = (-B) / (2 * A);

			bool b = true;
			Intersection in = Intersection(true, this, &line, t);

			return in;
		}
		else {
			const auto t1 = (-B + sqrt(delta)) / (2 * A);
			const auto t2 = (-B - sqrt(delta)) / (2 * A);

			Vector v1(line.dx().x() * t1 + line.x0().x(),
				line.dx().y() * t1 + line.x0().y(),
				line.dx().z() * t1 + line.x0().z());
			Vector v2(line.dx().x() * t2 + line.x0().x(),
				line.dx().y() * t2 + line.x0().y(),
				line.dx().z() * t2 + line.x0().z());

			const float t = std::max(t1, t2);

			if(t < minDist || t > maxDist)
				return Intersection(false, this, &line, 0);

			if (v1.length() < v2.length()) {
				return Intersection(true, this, &line, t1);
			}
			else
				return Intersection(true, this, &line, t2);
		}

    
}


const Vector Sphere::normal(const Vector& vec) const {
    Vector n = vec - _center;
    n.normalize();
    return n;
}
